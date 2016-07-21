using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimchaFund.Data;
using SimchaFund.Models;

namespace SimchaFund.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Simchas(int? id)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            SimchaViewModel model = new SimchaViewModel();
            model.Simchas = manager.GetSimchas(id).Select(s => new SimchaPageData
            {
                Simchas = s,
                ContributorCount = manager.GetTotalcontributors(),
                ContributedCount = manager.GetTotalPeopleContributed(s.Id),
                TotalContribution = manager.GetTotalContributionsForSimcha(s.Id),
            });
            return View(model);
        }
        public ActionResult Contribute(int id, int? contributorId)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            ContributeViewModel model = new ContributeViewModel();
            model.Simcha = manager.GetASimchas(id);
            model.Contributors = manager.GetContributors(contributorId).Select(c => new ContributionPageProperties
            {
                Contributer = c,
                Balance = manager.GetBalance(c.Id),
                AmountContributed = manager.GetContributionAmount(id, c.Id),
            });
            return View(model);
        }
        public ActionResult AddSimcha(string name, DateTime date)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            manager.AddSimcha(new Simchas
            {
                Name = name,
                Date = date
            });
            return Redirect("Simchas");
        }
        public ActionResult Contributors(int? id)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            ContributorsViewModel model = new ContributorsViewModel();
            model.Contributer = manager.GetContributors(id).Select(c => new ContributorPageProperties
            {
                Contributor = c,
                Balance = manager.GetBalance(c.Id),
            });
            model.TotalBalance = (double)manager.GetTotalBalance();
            return View(model);
        }
        public ActionResult Deposit(int id, double amount)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            manager.AddDeposit(id, amount);
            return Redirect("Contributors");
        }
        public ActionResult EditContributor(Contributors contributor)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            manager.UpdateContributor(contributor);
            return Redirect("Contributors");
        }
        public ActionResult AddContributor(Contributors contributor)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            manager.AddContributor(new Contributors
            {
                FirstName = contributor.FirstName,
                LastName = contributor.LastName,
                PhoneNum = contributor.PhoneNum,
                AlwaysInclude = contributor.AlwaysInclude,
            });
            return Redirect("Contributors");
        }
        public ActionResult ShowHistory(int contributorId, int? simchaId)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            ShowHistoryViewModel model = new ShowHistoryViewModel();
            string firstName = manager.GetContributors(contributorId).FirstOrDefault(c => c.Id == contributorId).FirstName;
            string lastName = manager.GetContributors(contributorId).FirstOrDefault(c => c.Id == contributorId).LastName;
            model.ContributorName = firstName + " " + lastName;
            model.Balance = manager.GetBalance(contributorId);
            model.History = manager.GetDeposits(contributorId).Select(d => new ShowHistoryProperties
            {
                Name = "Deposit",
                Date = d.Date,
                Amount = d.Amount,
            }).Concat(manager.GetSimchaAndContributors(contributorId, simchaId).Select(s => new ShowHistoryProperties
             {
                 Name = "Contribution To The " + manager.GetASimchas(s.SimchaId).Name + " Simcha",
                 Date = s.Date,
                 Amount = s.Contribution,
             })).OrderByDescending(h => h.Date);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddContribution(List<ContributeForm> form, int simchaId)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            foreach (ContributeForm cF in form)
            {
                if (cF.Contribute && manager.GetSimchaAndContributors(cF.ContributorId, simchaId).Count()== 0)
                {                   
                    manager.AddContribution(cF.ContributorId, cF.Contribution, simchaId);
                }
                else if (cF.Contribute && cF.Contribution != (double)manager.GetSimchaAndContributors(cF.ContributorId, simchaId).FirstOrDefault().Contribution)
                {
                    manager.UpdateContribution(cF.ContributorId, cF.Contribution, simchaId);
                }
                else if (!cF.Contribute)
                {
                    manager.DeleteContribution(cF.ContributorId, simchaId);
                }

            }
            return Redirect("Simchas");
        }
        public ActionResult Email(int? contributorId, int? simchaId)
        {
            SimchaFundManager manager = new SimchaFundManager(Properties.Settings.Default.ConStr);
            EmailViewModel model = new EmailViewModel();
            IEnumerable<SimchaAndContributors> sam = manager.GetSimchaAndContributors(contributorId, simchaId);
            model.Contributors = sam.Select(c => manager.GetContributors(c.ContributorId).FirstOrDefault()).OrderBy(c => c.LastName);
            model.Simcha = manager.GetSimchas(simchaId).FirstOrDefault().Name;
            return View(model);
        }
    }
}
