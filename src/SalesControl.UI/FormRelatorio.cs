using Microsoft.Reporting.WinForms;
using SalesControl.Application.Sales.DTOs;
using MediatR;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SalesControl.UI
{
    public partial class FormRelatorio : Form
    {
        private readonly IMediator _mediator;

        public FormRelatorio(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            InitializeComponent();

            // configure report resource and processing mode (designer must contain a ReportViewer named 'reportViewer')
            if (reportViewer != null)
            {
                reportViewer.ProcessingMode = ProcessingMode.Local;
                // use the embedded report resource name (Report1.rdlc is the file in Relatorios folder)
                reportViewer.LocalReport.ReportEmbeddedResource = "SalesControl.UI.Relatorios.Report1.rdlc";
            }

            // button click handled by designer event handler (btnGenerate_Click)
        }

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            // defaults: last 30 days
            dtpEnd.Value = DateTime.Today;
            dtpStart.Value = DateTime.Today.AddDays(-30);
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            await GenerateReportAsync();
        }

        private void BindReportWithDataTable(IEnumerable<SaleReportRowDto> rows)
        {
            var table = new DataTable();
            table.Columns.Add("SaleId", typeof(string));
            table.Columns.Add("SaleDate", typeof(DateTime));
            table.Columns.Add("ClientName", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UnitPrice", typeof(decimal));
            table.Columns.Add("LineTotal", typeof(decimal));

            foreach (var r in rows)
            {
                var saleDate = r.SaleDate.UtcDateTime;
                table.Rows.Add(r.SaleId.ToString(), saleDate, r.ClientName, r.ProductName, r.Quantity, r.UnitPrice, r.LineTotal);
            }

            reportViewer.LocalReport.DataSources.Clear();

            var reportDataSource = new ReportDataSource("DataSet1", table);
            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            reportViewer.RefreshReport();
        }

        private async Task GenerateReportAsync()
        {
            var start = dtpStart.Value.Date;
            var end = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);

            try
            {
                var result = await _mediator.Send(new SalesControl.Application.Sales.Queries.GetSalesReportQuery(start, end));
                if (!result.IsSuccess)
                {
                    var details = result.Errors is not null ? string.Join("\n", result.Errors) : "";
                    MessageBox.Show($"Erro ao obter dados do relatório.\n{details}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var rows = result.Value;
                BindReportWithDataTable(rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
