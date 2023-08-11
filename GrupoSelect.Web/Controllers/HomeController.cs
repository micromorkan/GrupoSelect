using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;
using GrupoSelect.Web.Helpers;
using GrupoSelect.Web.Util;
using GrupoSelect.Web.ViewModel;
using GrupoSelect.Web.Views.Shared.Components.Models;
using GrupoSelect.Web.Views.Shared.Reports.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorEngine;
using RazorEngine.Templating;
using System.Diagnostics;
using System.Globalization;

namespace GrupoSelect.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IContractService _contractService;
        private IProposalService _proposalService;
        private IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IContractService contractService, IProposalService proposalService, IUserService userService)
        {
            _logger = logger;
            _contractService = contractService;
            _proposalService = proposalService;
            _userService = userService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ComponentsVM dashboard = new ComponentsVM();

            string userProfile = User.GetProfile();

            if (userProfile != Constants.PROFILE_ADVOGADO && userProfile != Constants.PROFILE_TI)
            {
                dashboard.LstChart.Add(await MontarChartContratoSemanal());
                dashboard.LstChart.Add(await MontarChartContratoMensal());

                if (userProfile == Constants.PROFILE_REPRESENTANTE)
                {
                    dashboard.LstTile.Add(await MontarTileContratoCreditoMensal());
                }

                if (userProfile == Constants.PROFILE_DIRETOR || userProfile == Constants.PROFILE_GERENTE)
                {
                    var userList = (await _userService.GetAll(new Domain.Entity.User())).Object.ToList();

                    for (int i = 0; i < userList.Count(); i++)
                    {
                        if (userList[i].Profile == Constants.PROFILE_REPRESENTANTE || userList[i].Profile == Constants.PROFILE_GERENTE)
                        {
                            dashboard.LstTile.Add(await MontarTileContratoMensal(i + 1, userList[i].Id));
                        }
                    }
                }
            }

            return View(dashboard);
        }

        private async Task<Chart> MontarChartContratoSemanal()
        {
            string userProfile = User.GetProfile();
            int userId = Convert.ToInt32(User.GetId());

            DateTime startOfWeek = DateTime.Now;

            int days = CalculateOffset(startOfWeek.DayOfWeek, DayOfWeek.Thursday);

            startOfWeek = startOfWeek.AddDays(days - 7);

            PaginateResult<IEnumerable<Contract>> result = null;

            if (userProfile == Constants.PROFILE_DIRETOR || userProfile == Constants.PROFILE_GERENTE || userProfile == Constants.PROFILE_ADMINISTRATIVO)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal() }, 1, 1000, startOfWeek, startOfWeek.AddDays(6));
            }
            else if (userProfile == Constants.PROFILE_REPRESENTANTE)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal { UserId = userId } }, 1, 1000, startOfWeek, startOfWeek.AddDays(6));
            }

            string[] status = (Constants.CONTRACT_STATUS_AD + "," + Constants.CONTRACT_STATUS_PA + "," + Constants.CONTRACT_STATUS_CA + "," + Constants.CONTRACT_STATUS_CR + "," + Constants.CONTRACT_STATUS_CC).Split(",");
            string[] colors = (Constants.SYSTEM_RGBA_GREEN + "|" + Constants.SYSTEM_RGBA_ORANGE + "|" + Constants.SYSTEM_RGBA_BLUE + "|" + Constants.SYSTEM_RGBA_RED + "|" + Constants.SYSTEM_RGBA_PURPLE).Split("|");
            int[] values = new int[5]
            {
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_AD),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_PA),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CA),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CR),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CC)
            };

            Chart contratosSemanal = new Chart
            {
                Id = 1,
                Titulo = "Contratos - Semanal (" + startOfWeek.Date.ToShortDateString() + " - " + startOfWeek.AddDays(6).Date.ToShortDateString() + ")",
                Cores = colors,
                Textos = status,
                Valores = values,
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
                Controller = "Home",
                Action = "AtualizarContratosSemanal",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500 //600000
            };
            return contratosSemanal;
        }

        private async Task<Chart> MontarChartContratoMensal()
        {
            string userProfile = User.GetProfile();
            int userId = Convert.ToInt32(User.GetId());

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            PaginateResult<IEnumerable<Contract>> result = null;

            if (userProfile == Constants.PROFILE_DIRETOR || userProfile == Constants.PROFILE_GERENTE || userProfile == Constants.PROFILE_ADMINISTRATIVO)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal() }, 1, 1000, firstDayOfMonth, lastDayOfMonth);
            }
            else if (userProfile == Constants.PROFILE_REPRESENTANTE)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal { UserId = userId } }, 1, 1000, firstDayOfMonth, lastDayOfMonth);
            }

            string[] status = (Constants.CONTRACT_STATUS_AD + "," + Constants.CONTRACT_STATUS_PA + "," + Constants.CONTRACT_STATUS_CA + "," + Constants.CONTRACT_STATUS_CR + "," + Constants.CONTRACT_STATUS_CC).Split(",");
            string[] colors = (Constants.SYSTEM_RGBA_GREEN + "|" + Constants.SYSTEM_RGBA_ORANGE + "|" + Constants.SYSTEM_RGBA_BLUE + "|" + Constants.SYSTEM_RGBA_RED + "|" + Constants.SYSTEM_RGBA_PURPLE).Split("|");
            int[] values = new int[5]
            {
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_AD),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_PA),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CA),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CR),
                result.Object.Count(x => x.Status == Constants.CONTRACT_STATUS_CC)
            };

            Chart contratosSemanal = new Chart
            {
                Id = 2,
                Titulo = "Contratos - Mensal (" + firstDayOfMonth.Date.ToShortDateString() + " - " + lastDayOfMonth.Date.ToShortDateString() + ")",
                Cores = colors,
                Textos = status,
                Valores = values,
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
                Controller = "Home",
                Action = "AtualizarContratosMensal",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500 //600000
            };
            return contratosSemanal;
        }

        private async Task<Tile> MontarTileContratoMensal(int id, int userId)
        {
            string userProfile = User.GetProfile();

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var result = await _contractService.GetAllPaginate(new Contract { Status = Constants.CONTRACT_STATUS_CA, Proposal = new Proposal { UserId = userId } }, 1, 1000, firstDayOfMonth, lastDayOfMonth);

            Tile tile = new Tile();

            tile.Id = id;
            tile.BackgroundColor = Constants.SYSTEM_RGBA_WHITE;
            tile.Icone = "fa-file-text-o";
            tile.Descricao = string.Empty;
            tile.Titulo = (await _userService.GetById(userId)).Object.Representation;
            tile.Valor = result.Object != null ? result.Object.Count().ToString() + " Contratos" : "0 Contratos";
            tile.Controller = "Home";
            tile.Action = "AtualizarTileContratoMensal";
            tile.IntervaloAtualizacao = 5000;
            tile.Filter = userId;

            return tile;
        }

        private async Task<Tile> MontarTileContratoCreditoMensal()
        {
            int userId = Convert.ToInt32(User.GetId());

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var result = await _contractService.GetAllPaginate(new Contract { Status = Constants.CONTRACT_STATUS_CA, Proposal = new Proposal { UserId = userId } }, 1, 1000, firstDayOfMonth, lastDayOfMonth);

            Tile tile = new Tile();

            tile.Id = 1;
            tile.BackgroundColor = Constants.SYSTEM_RGBA_WHITE;
            tile.Icone = "fa-money";
            tile.Descricao = string.Empty;
            tile.Titulo = "Total de Vendas";
            tile.Valor = result.Object.Count() > 0 ? string.Format("{0:C}", result.Object.Sum(x => Convert.ToDecimal(x.Proposal.CreditValue))) : "R$ 0,00";
            tile.Controller = "Home";
            tile.Action = "AtualizarTileContratoCreditoMensal";
            tile.IntervaloAtualizacao = 5000;
            tile.Filter = null;

            return tile;
        }

        public int CalculateOffset(DayOfWeek current, DayOfWeek desired)
        {
            int c = (int)current;
            int d = (int)desired;
            int offset = (7 - c + d) % 7;
            return offset == 0 ? 7 : offset;
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarContratosSemanal()
        {
            return Json(await MontarChartContratoSemanal());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarContratosMensal()
        {
            return Json(await MontarChartContratoMensal());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarTileContratoMensal(int id, string filter)
        {
            return Json(await MontarTileContratoMensal(id, Convert.ToInt32(filter)));
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarTileContratoCreditoMensal()
        {
            return Json(await MontarTileContratoCreditoMensal());
        }

        #endregion

        #region FINANCEIRO

        [Authorize(Roles = Constants.PROFILE_ADMINISTRATIVO + "," + Constants.PROFILE_GERENTE + "," + Constants.PROFILE_DIRETOR)]
        public async Task<IActionResult> Financeiro()
        {
            ComponentsVM dashboard = new ComponentsVM();

            string userProfile = User.GetProfile();


            if (userProfile != Constants.PROFILE_ADVOGADO && userProfile != Constants.PROFILE_TI)
            {
                if (userProfile == Constants.PROFILE_DIRETOR)
                {
                    dashboard.LstTile.Add(await MontarTileContratoFinanceiroMensal());
                }

                dashboard.LstChartMoney.Add(await MontarChartContratoFinanceiroSemanal());
                dashboard.LstChartMoney.Add(await MontarChartContratoFinanceiroMensal());
                dashboard.LstChartMoney.Add(await MontarChartContratoFinanceiroTriMestral());
            }

            return View(dashboard);
        }

        private async Task<ChartMoney> MontarChartContratoFinanceiroSemanal()
        {
            string userProfile = User.GetProfile();
            int userId = Convert.ToInt32(User.GetId());

            DateTime startOfWeek = DateTime.Now;

            int days = CalculateOffset(startOfWeek.DayOfWeek, DayOfWeek.Thursday);

            startOfWeek = startOfWeek.AddDays(days - 7);

            PaginateResult<IEnumerable<Contract>> result = null;

            if (userProfile == Constants.PROFILE_DIRETOR || userProfile == Constants.PROFILE_GERENTE || userProfile == Constants.PROFILE_ADMINISTRATIVO)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal(), Status = Constants.CONTRACT_STATUS_CA }, 1, 1000, startOfWeek, startOfWeek.AddDays(6));
            }

            List<Contract> listContracts = result.Object.ToList();

            string[] users = listContracts.GroupBy(s => s.Proposal.User.Representation)
                           .Select(g => new
                           {
                               User = g.Key,
                               Sales = g.Sum(s => Convert.ToDecimal(s.Proposal.CreditValue))
                           })
                           .OrderByDescending(r => r.Sales)
                           .Select(x => x.User).ToArray();

            string[] colors = new string[users.Length];
            decimal[] values = new decimal[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                string rgb = (220 - (i * 20)).ToString();
                colors[i] = "rgba(" + rgb + ", " + rgb + ", " + rgb + ", 1)";
                values[i] = listContracts.Where(x => x.Proposal.User.Representation == users[i]).Sum(x => Convert.ToDecimal(x.Proposal.CreditValue));
            }

            ChartMoney contratosFinanceiroSemanal = new ChartMoney
            {
                Id = 1,
                Titulo = "Parcial - Semanal (" + startOfWeek.Date.ToShortDateString() + " - " + startOfWeek.AddDays(6).Date.ToShortDateString() + ")",
                Cores = colors,
                Textos = users,
                Valores = values,
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
                Controller = "Home",
                Action = "AtualizarChartContratoFinanceiroSemanal",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            return contratosFinanceiroSemanal;
        }

        private async Task<ChartMoney> MontarChartContratoFinanceiroMensal()
        {
            string userProfile = User.GetProfile();
            int userId = Convert.ToInt32(User.GetId());

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            PaginateResult<IEnumerable<Contract>> result = null;

            if (userProfile == Constants.PROFILE_DIRETOR || userProfile == Constants.PROFILE_GERENTE || userProfile == Constants.PROFILE_ADMINISTRATIVO)
            {
                result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal(), Status = Constants.CONTRACT_STATUS_CA }, 1, 1000, firstDayOfMonth, lastDayOfMonth);
            }

            List<Contract> listContracts = result.Object.ToList();

            string[] users = listContracts.GroupBy(s => s.Proposal.User.Representation)
                           .Select(g => new
                           {
                               User = g.Key,
                               Sales = g.Sum(s => Convert.ToDecimal(s.Proposal.CreditValue))
                           })
                           .OrderByDescending(r => r.Sales)
                           .Select(x => x.User).ToArray();

            string[] colors = new string[users.Length];
            decimal[] values = new decimal[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                string rgb = (220 - (i * 20)).ToString();
                colors[i] = "rgba(" + rgb + ", " + rgb + ", " + rgb + ", 1)";
                values[i] = listContracts.Where(x => x.Proposal.User.Representation == users[i]).Sum(x => Convert.ToDecimal(x.Proposal.CreditValue));
            }

            ChartMoney contratosFinanceiroSemanal = new ChartMoney
            {
                Id = 2,
                Titulo = "Mensal (" + firstDayOfMonth.Date.ToShortDateString() + " - " + lastDayOfMonth.ToShortDateString() + ")",
                Cores = colors,
                Textos = users,
                Valores = values,
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
                Controller = "Home",
                Action = "AtualizarChartContratoFinanceiroMensal",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            return contratosFinanceiroSemanal;
        }

        private async Task<ChartMoney> MontarChartContratoFinanceiroTriMestral()
        {
            string userProfile = User.GetProfile();
            int userId = Convert.ToInt32(User.GetId());

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            string[] months = new string[3];
            string[] colors = new string[3];
            decimal[] values = new decimal[3];

            for (int i = -2; i < 1; i++)
            {
                var firstMonth = firstDayOfMonth.AddMonths(i);
                var lastDayOfMonth = firstMonth.AddMonths(1).AddDays(-1);

                months[i + 2] = firstMonth.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-BR")).ToUpper();
                colors[i + 2] = "rgba(221, 221, 221, 1)";

                PaginateResult<IEnumerable<Contract>> result = await _contractService.GetAllPaginate(new Contract { Proposal = new Proposal(), Status = Constants.CONTRACT_STATUS_CA }, 1, 1000, firstMonth, lastDayOfMonth);

                if (result.Object.Count() > 0)
                {
                    List<Contract> listContracts = result.Object.ToList();

                    values[i + 2] = listContracts.Sum(x => Convert.ToDecimal(x.Proposal.CreditValue));
                }
                else
                {
                    values[i + 2] = 0;
                }
            }

            ChartMoney contratosFinanceiroSemanal = new ChartMoney
            {
                Id = 3,
                Titulo = "Renda Bruta dos últimos 3 meses",
                Cores = colors,
                Textos = months,
                Valores = values,
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraVertical),
                Controller = "Home",
                Action = "AtualizarChartContratoFinanceiroTriMestral",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            return contratosFinanceiroSemanal;
        }

        private async Task<Tile> MontarTileContratoFinanceiroMensal()
        {
            string userProfile = User.GetProfile();

            DateTime date = DateTime.Now;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var result = await _contractService.GetAllPaginate(new Contract { Status = Constants.CONTRACT_STATUS_CA, Proposal = new Proposal() }, 1, 1000, firstDayOfMonth, lastDayOfMonth);

            Tile tile = new Tile();

            tile.Id = 1;
            tile.BackgroundColor = Constants.SYSTEM_RGBA_WHITE;
            tile.Icone = "fa-money";
            tile.Descricao = string.Empty;
            tile.Titulo = "Faturamento Mensal";
            tile.Valor = result.Object.Count() > 0 ? string.Format("{0:C}", result.Object.Sum(x => Convert.ToDecimal(x.Proposal.CreditTotalValue))) : "R$ 0,00"; 
            tile.Controller = "Home";
            tile.Action = "AtualizarTileContratoFinanceiroMensal";
            tile.IntervaloAtualizacao = 5000;
            tile.Filter = null;

            return tile;
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarChartContratoFinanceiroSemanal()
        {
            return Json(await MontarChartContratoFinanceiroSemanal());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarChartContratoFinanceiroMensal()
        {
            return Json(await MontarChartContratoFinanceiroMensal());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarChartContratoFinanceiroTriMestral()
        {
            return Json(await MontarChartContratoFinanceiroTriMestral());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarTileContratoFinanceiroMensal()
        {
            return Json(await MontarTileContratoFinanceiroMensal());
        }

        #endregion

        #region EXEMPLOS COMPONENTES

        public async Task<IActionResult> Components()
        {
            ComponentsVM index = new ComponentsVM();

            #region FILLBAR

            List<FillBar> lstBar = new List<FillBar>();

            lstBar.Add(new FillBar
            {
                Id = 1,
                Titulo = "Consumo Mensal",
                Descricao = "Seu saldo expira em 10 dias!",
                ValorReal = "72%",
                ValorPorcentagem = 72,
                BarColor = "#0404B4",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 5500
            });

            lstBar.Add(new FillBar
            {
                Id = 2,
                Titulo = "Mensagens Enviadas",
                Descricao = "O pacote será renovado em 18 dias!",
                ValorReal = "132 mensagens",
                ValorPorcentagem = 43,
                BarColor = "#FF0040",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 6500
            });

            lstBar.Add(new FillBar
            {
                Id = 3,
                Titulo = "Total Gasto",
                Descricao = "Seu limite é de R$ 5.000,00",
                ValorReal = "R$ 4.450,00",
                ValorPorcentagem = 89,
                BarColor = "#74DF00",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 7500
            });

            lstBar.Add(new FillBar
            {
                Id = 4,
                Titulo = "Pré-Pago Semanal",
                Descricao = "Smart Pre Local + 500mb",
                ValorReal = "350mb",
                ValorPorcentagem = 65,
                BarColor = "#74DF00",
                BadgeText = "R$ 9,99",
                BadgeColor = "#000099",
                Controller = "Home",
                Action = "AtualizarFill",
                IntervaloAtualizacao = 8500
            });

            index.LstFill = lstBar;

            #endregion

            #region CHART

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 1,
                Titulo = "1",
                Cores = new string[10] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)" },
                Textos = new string[10] { "Red", "Yellow", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue" },
                Valores = new int[10] { 15, 20, 30, 40, 50, 60, 70, 80, 90, 100 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
                Controller = "Home",
                Action = "AtualizarChart",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            index.LstChart.Add(chart);

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart2 = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 2,
                Titulo = "2",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { 15, 20, 30 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraVertical),
                Controller = "Home",
                Action = "AtualizarChart2",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            index.LstChart.Add(chart2);

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart3 = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 3,
                Titulo = "3",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { 15, 20, 30 },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
                Controller = "Home",
                Action = "AtualizarChart3",
                BackgroundColor = "#FFF",
                IntervaloAtualizacao = 8500
            };

            index.LstChart.Add(chart3);

            #endregion

            #region TILE

            Tile tile = new Tile();

            tile.Id = 1;
            tile.BackgroundColor = "#FFF";
            tile.Icone = "fa-user";
            tile.Descricao = "Descrição";
            tile.Titulo = "Usuarios Ativos";
            tile.Valor = "15";
            tile.Controller = "Home";
            tile.Action = "AtualizarTile";
            tile.IntervaloAtualizacao = 5000;

            index.LstTile.Add(tile);

            Tile tile2 = new Tile();
            tile2.Id = 2;
            tile2.BackgroundColor = "#99ffcc";
            tile2.Icone = "fa-edit";
            tile2.Descricao = "Descrição";
            tile2.Titulo = "Casos abertos";
            tile2.Valor = "234";
            tile2.Controller = "Home";
            tile2.Action = "AtualizarTile2";
            tile2.IntervaloAtualizacao = 8000;

            index.LstTile.Add(tile2);

            Tile tile3 = new Tile();
            tile3.Id = 3;
            tile3.BackgroundColor = "#ffd9b3";
            tile3.Icone = "fa-folder";
            tile3.Descricao = "Descrição";
            tile3.Titulo = "Arquivos Pendentes";
            tile3.Valor = "56";
            tile3.Controller = "Home";
            tile3.Action = "AtualizarTile3";
            tile3.IntervaloAtualizacao = 6500;
            index.LstTile.Add(tile3);

            #endregion

            #region CALENDAR

            Views.Shared.Components.Models.Calendar calendario = new Views.Shared.Components.Models.Calendar();

            calendario.IdUsuario = 1;
            calendario.NomeUsuario = "Diego Andrade Sampaio";
            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 1,
                Titulo = "Evento 1",
                Descricao = "Descricao evento 1",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 2,
                Titulo = "Evento 2",
                Descricao = "Descricao evento 2",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Cancelado
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 3,
                Titulo = "Evento 3",
                Descricao = "Descricao evento 3",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(1)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Eventos.Add(new CalendarEvent
            {
                Id = 4,
                Titulo = "Evento 4",
                Descricao = "Descricao evento 4",
                DataIniEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(2)),
                DataFimEvento = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:F}", DateTime.Now.AddDays(-1).AddHours(3)),
                DiaInteiro = false,
                Status = (int)UtilWebEnums.StatusEvento.Ativo
            });

            calendario.Acoes.ControllerAtualizarCalendar = "Home";
            calendario.Acoes.ActionAtualizarCalendar = "AtualizarCalendario";

            index.Calendar = calendario;

            #endregion

            return View(index);
        }

        [HttpPost]
        public JsonResult AtualizarTile()
        {
            Tile tile = new Tile();

            tile.BackgroundColor = "";
            tile.Descricao = "Descrição";
            tile.Titulo = "Usuarios Cadastrados";
            tile.Valor = new Random().Next(6, 25).ToString();
            tile.IntervaloAtualizacao = 5000;

            return Json(tile);
        }

        [HttpPost]
        public JsonResult AtualizarTile2()
        {
            Tile tile2 = new Tile();
            tile2.BackgroundColor = "#99ffcc";
            tile2.Icone = "fa-database";
            tile2.Descricao = "Descrição";
            tile2.Titulo = "Casos abertos";
            tile2.Valor = new Random().Next(230, 250).ToString();
            tile2.IntervaloAtualizacao = 10000;

            return Json(tile2);
        }

        [HttpPost]
        public JsonResult AtualizarTile3()
        {
            Tile tile3 = new Tile();
            tile3.Id = 3;
            tile3.BackgroundColor = "#ffd9b3";
            tile3.Icone = "fa-edit";
            tile3.Descricao = "Descrição";
            tile3.Titulo = "Arquivos Pendentes";
            tile3.Valor = new Random().Next(40, 68).ToString();
            tile3.IntervaloAtualizacao = 6500;


            return Json(tile3);
        }

        [HttpPost]
        public JsonResult AtualizarChart()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);
            var d = new Random().Next(31, 40);
            var e = new Random().Next(41, 50);
            var f = new Random().Next(51, 60);
            var g = new Random().Next(61, 70);
            var h = new Random().Next(71, 80);
            var i = new Random().Next(81, 90);
            var j = new Random().Next(91, 100);

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 1,
                Titulo = "1",
                Cores = new string[10] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)", "rgba(255, 206, 86, 1)" },
                Textos = new string[10] { "Red", "Yellow", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue", "Blue" },
                Valores = new int[10] { j, i, h, g, f, e, d, c, b, a },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraHorizontal),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarChart2()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 2,
                Titulo = "2",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { a, b, c },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.BarraVertical),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarChart3()
        {
            var a = new Random().Next(1, 10);
            var b = new Random().Next(11, 20);
            var c = new Random().Next(21, 30);

            GrupoSelect.Web.Views.Shared.Components.Models.Chart chart = new GrupoSelect.Web.Views.Shared.Components.Models.Chart
            {
                Id = 3,
                Titulo = "3",
                Cores = new string[3] { "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(54, 162, 235, 1)" },
                Textos = new string[3] { "Red", "Yellow", "Blue" },
                Valores = new int[3] { a, b, c },
                PermiteMinimizar = true,
                TipoChart = UtilWeb.GetEnumDescription(UtilWebEnums.TipoChart.Torta),
            };

            return Json(chart);
        }

        [HttpPost]
        public JsonResult AtualizarFill()
        {
            var a = new Random().Next(1, 100);

            FillBar chart = new FillBar
            {
                ValorPorcentagem = a,
            };

            return Json(chart);
        }

        #endregion

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}