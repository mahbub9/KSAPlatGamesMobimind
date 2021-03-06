﻿using ForestInterActive;
using PlatGames.DAL;
using PlatGames.DAL.DataRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlatGame.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string msisdn)
        {
            if (IsSubscribed(msisdn))
            {
                FormsAuthentication.SetAuthCookie(msisdn, true);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Subscribe", "Home");
        }

        private bool IsSubscribed(string msisdn)
        {
            string phoneNo = string.Empty;
            int length = msisdn.Length;
            if (length >= 9)
            {
                phoneNo = string.Concat("966", msisdn.Substring(length - 9, 9));
            }

            var subscriber = new SubscriptionRepo().FindBy(x => x.Msisdn == phoneNo && x.IsSubscribed == true).FirstOrDefault();

            return subscriber != null ? true : false;
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}