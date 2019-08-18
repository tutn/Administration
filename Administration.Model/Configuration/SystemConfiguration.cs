using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Model.Configuration
{
    public class SystemConfiguration
    {
        private static string _prefixc;
        public static string PREFIXC
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_prefixc)) return _prefixc;
                _prefixc = ConfigurationManager.AppSettings["PREFIXC"];
                if (string.IsNullOrWhiteSpace(_prefixc))
                {
                    _prefixc = "__";
                }
                return _prefixc;
            }
        }

        private static List<string> _userNotLdap;
        public static List<string> UserNotLdap
        {
            get
            {
                if (_userNotLdap != null && _userNotLdap.Count > 0)
                {
                    return _userNotLdap;
                }
                var key = ConfigurationManager.AppSettings.AllKeys.FirstOrDefault(x => x.ToUpper() == "USERNOTLDAP");
                if (!string.IsNullOrEmpty(key)) _userNotLdap = ConfigurationManager.AppSettings[key].Split(';').Select(x => x.Trim()).ToList();
                return _userNotLdap;
            }
        }

        private static string _preDefaultPassword;
        public static string PREDEFAULTPASSWORD
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_preDefaultPassword)) return _preDefaultPassword;
                _preDefaultPassword = ConfigurationManager.AppSettings["PREDEFAULTPASSWORD"];
                if (string.IsNullOrWhiteSpace(_preDefaultPassword))
                {
                    _preDefaultPassword = "VPBank";
                }
                return _preDefaultPassword;
            }
        }

        #region Email
        private static string _fromAddress;
        public static string FromAddress
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_fromAddress)) return _fromAddress;
                _fromAddress = ConfigurationManager.AppSettings["FromAddress"];
                if (string.IsNullOrWhiteSpace(_fromAddress))
                {
                    _fromAddress = "tudd1@vpbank.com.vn";
                }
                return _fromAddress;
            }
        }

        private static string _emailCredential;
        public static string EmailCredential
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_emailCredential)) return _emailCredential;
                _emailCredential = ConfigurationManager.AppSettings["EmailCredential"];
                if (string.IsNullOrWhiteSpace(_emailCredential))
                {
                    _emailCredential = "tutn5@vpbank.com.vn";
                }
                return _emailCredential;
            }
        }

        private static string _passwordCredential;
        public static string PasswordCredential
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_passwordCredential)) return _passwordCredential;
                _passwordCredential = ConfigurationManager.AppSettings["PasswordCredential"];
                if (string.IsNullOrWhiteSpace(_passwordCredential))
                {
                    _passwordCredential = "vianh@2013";
                }
                return _passwordCredential;
            }
        }

        private static string _emailHost;
        public static string EmailHost
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_emailHost)) return _emailHost;
                _emailHost = ConfigurationManager.AppSettings["EmailHost"];
                if (string.IsNullOrWhiteSpace(_emailHost))
                {
                    _emailHost = "smtp.office365.com";
                }
                return _emailHost;
            }
        }

        private static int _emailPort;
        public static int EmailPort
        {
            get
            {
                if (_emailPort > 0) return _emailPort;
                var _port = ConfigurationManager.AppSettings["EmailPort"];
                if (string.IsNullOrWhiteSpace(_port))
                {
                    _emailPort = 25;
                }
                else
                {
                    _emailPort = int.Parse(_port);
                }
                return _emailPort;
            }
        }
        #endregion

        #region Job
        private static int _riceCategory;
        public static int RiceCategory
        {
            get
            {
                if (_riceCategory > 0) return _riceCategory;
                var _rice = ConfigurationManager.AppSettings["RiceCategory"];
                if (string.IsNullOrWhiteSpace(_rice))
                {
                    _riceCategory = 62;
                }
                else
                {
                    _riceCategory = int.Parse(_rice);
                }
                return _riceCategory;
            }
        }

        private static int _coffeeCategory;
        public static int CoffeeCategory
        {
            get
            {
                if (_coffeeCategory > 0) return _coffeeCategory;
                var _coffee = ConfigurationManager.AppSettings["CoffeeCategory"];
                if (string.IsNullOrWhiteSpace(_coffee))
                {
                    _coffeeCategory = 69;
                }
                else
                {
                    _coffeeCategory = int.Parse(_coffee);
                }
                return _coffeeCategory;
            }
        }

        private static int _pepperCategory;
        public static int PepperCategory
        {
            get
            {
                if (_pepperCategory > 0) return _pepperCategory;
                var _pepper = ConfigurationManager.AppSettings["PepperCategory"];
                if (string.IsNullOrWhiteSpace(_pepper))
                {
                    _pepperCategory = 70;
                }
                else
                {
                    _pepperCategory = int.Parse(_pepper);
                }
                return _pepperCategory;
            }
        }

        private static int _seafoodCategory;
        public static int SeaFoodCategory
        {
            get
            {
                if (_seafoodCategory > 0) return _seafoodCategory;
                var _vasep = ConfigurationManager.AppSettings["SeaFoodCategory"];
                if (string.IsNullOrWhiteSpace(_vasep))
                {
                    _seafoodCategory = 71;
                }
                else
                {
                    _seafoodCategory = int.Parse(_vasep);
                }
                return _seafoodCategory;
            }
        }

        private static int _seafoodmkCategory;
        public static int SeaFoodMarKetCategory
        {
            get
            {
                if (_seafoodmkCategory > 0) return _seafoodmkCategory;
                var _sm = ConfigurationManager.AppSettings["SeaFoodMarKetCategory"];
                if (string.IsNullOrWhiteSpace(_sm))
                {
                    _seafoodmkCategory = 72;
                }
                else
                {
                    _seafoodmkCategory = int.Parse(_sm);
                }
                return _seafoodmkCategory;
            }
        }

        private static int _coffeeAgmCategory;
        public static int CoffeeAgmCategory
        {
            get
            {
                if (_coffeeAgmCategory > 0) return _coffeeAgmCategory;
                var _sm = ConfigurationManager.AppSettings["PepperAgmCategory"];
                if (string.IsNullOrWhiteSpace(_sm))
                {
                    _coffeeAgmCategory = 73;
                }
                else
                {
                    _coffeeAgmCategory = int.Parse(_sm);
                }
                return _coffeeAgmCategory;
            }
        }

        private static int _pepperAgmCategory;
        public static int PepperAgmCategory
        {
            get
            {
                if (_pepperAgmCategory > 0) return _pepperAgmCategory;
                var _sm = ConfigurationManager.AppSettings["PepperAgmCategory"];
                if (string.IsNullOrWhiteSpace(_sm))
                {
                    _pepperAgmCategory = 74;
                }
                else
                {
                    _pepperAgmCategory = int.Parse(_sm);
                }
                return _pepperAgmCategory;
            }
        }

        #endregion

        #region Banks
        private static string _agribank;
        public static string AGRIBANK
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_agribank)) return _agribank;
                var _bankcode = ConfigurationManager.AppSettings["Agribank"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _agribank = "Agribank";
                }
                else
                {
                    _agribank = _bankcode;
                }
                return _agribank;
            }
        }

        private static string _abbank;
        public static string ABBANK
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_abbank)) return _abbank;
                var _bankcode = ConfigurationManager.AppSettings["ABBANK"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _abbank = "ABBANK";
                }
                else
                {
                    _abbank = _bankcode;
                }
                return _abbank;
            }
        }

        private static string _acb;
        public static string ACB
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_acb)) return _acb;
                var _bankcode= ConfigurationManager.AppSettings["ACB"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _acb = "ACB";
                }
                else
                {
                    _acb = _bankcode;
                }
                return _acb;
            }
        }

        private static string _bidv;
        public static string BIDV
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_bidv)) return _bidv;
                var _bankcode = ConfigurationManager.AppSettings["BIDV"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _bidv = "BIDV";
                }
                else
                {
                    _bidv = _bankcode;
                }
                return _bidv;
            }
        }

        private static string _msb;
        public static string MSB
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_msb)) return _msb;
                var _bankcode = ConfigurationManager.AppSettings["MSB"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _msb = "MSB";
                }
                else
                {
                    _msb = _bankcode;
                }
                return _msb;
            }
        }

        private static string _tcb;
        public static string TCB
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_tcb)) return _tcb;
                var _bankcode = ConfigurationManager.AppSettings["TCB"];
                if (string.IsNullOrWhiteSpace(_bankcode))
                {
                    _tcb = "TCB";
                }
                else
                {
                    _tcb = _bankcode;
                }
                return _tcb;
            }
        }

        
        #endregion
    }
}
