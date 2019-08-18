using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Administration.Model.Utilities
{
    public static class Extensions
    {
        public static string SplitString(string input, char c, int index)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                var arr = input.Split(c);
                result = arr[index].Trim();

            }
            return result;
        }

        public static string SplitString(string input, string[] st, int index)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                var arr = input.Split(st, StringSplitOptions.None);
                result = arr[index];

            }
            return result;
        }

        public static string[] SplitStringToArray(string input, string[] st, int index)
        {
            var result = new string[] { };
            if (!string.IsNullOrWhiteSpace(input))
            {
                var arr = input.Split(st, StringSplitOptions.None);
                result = arr;

            }
            return result;
        }

        public static decimal? CalculateDecimalValues(string input)
        {
            decimal? result = (decimal?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input) && input.Trim() != "-")
            {
                if (input.Contains("*"))
                {
                    rsinput = SplitString(input, '*', 0);
                }
                else
                {
                    rsinput = input;
                }
                result = CalculateValue(rsinput);
            }
            return result;
        }

        public static double? CalculateDoubleValues(string input)
        {
            double? result = (double?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input) && input.Trim() != "-")
            {
                if (input.Contains("*"))
                {
                    rsinput = SplitString(input, '*', 0);
                }
                else
                {
                    rsinput = input;
                }
                var value = CalculateValue(rsinput);
                result = value != null ? Convert.ToDouble(value) : (double?)null;
            }
            return result;
        }

        public static decimal? CalculateValue(string value)
        {
            decimal? result = (decimal?)null;
            if (value.Contains("/"))
            {
                result = ReturnValue(value);
            }
            else
            {
                result = decimal.Parse(value);
            }

            return result;
        }

        public static decimal? ReturnValue(string input)
        {
            decimal? result = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    var arr = input.Split(' ');
                    var lastarr = arr[1].Split('/');
                    var pre_value = Convert.ToDecimal(arr[0]);
                    var lst_value = Convert.ToDecimal(lastarr[0]) / Convert.ToDecimal(lastarr[1]);
                    result = pre_value + lst_value;
                }
            }
            catch (Exception ex)
            {
                return (decimal?)null;
            }

            return result;
        }

        public static DateTime? ReturnDateTime(string input)
        {
            DateTime? result = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    switch (input.Length)
                    {
                        case 3:
                            result = DateTime.ParseExact(string.Format("{0}/{1}", input, DateTime.Now.Year), "d/M/yyyy", null);
                            break;
                        case 4:
                            var arrstr = input.Split('/');
                            if (arrstr[0].Length == 2)
                            {
                                result = DateTime.ParseExact(string.Format("{0}/{1}", input, DateTime.Now.Year), "dd/M/yyyy", null);
                            }
                            else
                            {
                                result = DateTime.ParseExact(string.Format("{0}/{1}", input, DateTime.Now.Year), "d/MM/yyyy", null);
                            }
                            break;
                        case 5:
                            result = DateTime.ParseExact(string.Format("{0}/{1}", input, DateTime.Now.Year), "dd/MM/yyyy", null);
                            break;
                        case 8:
                            result = DateTime.ParseExact(input, "d/M/yyyy", null);
                            break;
                        case 9:
                            var arrst = input.Split('/');
                            if (arrst[0].Length == 2)
                            {
                                result = DateTime.ParseExact(input, "dd/M/yyyy", null);
                            }
                            else if (arrst[1].Length == 2)
                            {
                                result = DateTime.ParseExact(input, "d/MM/yyyy", null);
                            }
                            break;
                        default:
                            result = DateTime.ParseExact(input, "dd/MM/yyyy", null);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return (DateTime?)null;
            }

            return result;
        }

        public static List<DateTime?> ConvertDateTime(string input)
        {
            var startdate = string.Empty;
            var enddate = string.Empty;
            string[] arrStartDate = null;
            string[] arrEnddate = null;
            DateTime? Startdate = null;
            DateTime? Enddate = null;
            var lst = new List<DateTime?>();
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    var rs = input.Split(' ');
                    if (rs != null && rs.Count() > 0)
                    {
                        enddate = rs[rs.Length - 1];
                        if (!string.IsNullOrWhiteSpace(enddate))
                        {
                            if (enddate.Contains("/"))
                            {
                                arrEnddate = enddate.Split('/');
                                enddate = string.Format("{0}/{1}/{2}", arrEnddate[1], arrEnddate[0], arrEnddate[2]);
                                Enddate = Convert.ToDateTime(enddate);

                                lst.Add(Enddate);
                            }
                        }
                        startdate = rs[rs.Length - 3];
                        if (!string.IsNullOrWhiteSpace(startdate))
                        {
                            if (startdate.Length > 7)
                            {
                                if (startdate.Contains("/"))
                                {
                                    arrStartDate = startdate.Split('/');
                                    startdate = string.Format("{0}/{1}/{2}", arrStartDate[1], arrStartDate[0], arrStartDate[2]);
                                    Startdate = Convert.ToDateTime(startdate);
                                }
                            }
                            else
                            {
                                if (startdate.Contains("/"))
                                {
                                    arrStartDate = startdate.Split('/');
                                    startdate = string.Format("{0}/{1}/{2}", arrStartDate[1], arrStartDate[0], arrEnddate[2]);
                                    Startdate = Convert.ToDateTime(startdate);
                                }
                            }

                            lst.Add(Startdate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<DateTime?> GetDateTimeSeaFoodMarket(string input)
        {
            var startdate = string.Empty;
            var endDate = string.Empty;

            DateTime? Startdate = null;
            DateTime? Enddate = null;
            var lst = new List<DateTime?>();
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    var rs = input.Split(' ');
                    if (rs != null && rs.Count() > 0)
                    {
                        var arrDate = rs[rs.Length - 1];
                        if (!string.IsNullOrWhiteSpace(arrDate))
                        {
                            if (arrDate.Contains("-"))
                            {
                                var arr = arrDate.Split('-');
                                Startdate = ReDateTime(arr[0]);
                                lst.Add(Startdate);
                                Enddate = ReDateTime(arr[1]);
                                lst.Add(Enddate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static DateTime? ReDateTime(string input)
        {
            DateTime? result = null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains("-"))
                {
                    var rs = input.Split('-');
                    input = rs[1];
                }
                if (input.Contains("/"))
                {
                    var arr = input.Split('/');
                    var day = arr[0].Length < 2 ? string.Format("0{0}", arr[0]) : arr[0];
                    var month = arr[1].Length < 2 ? string.Format("0{0}", arr[1]) : arr[1];
                    result = DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, DateTime.Now.Year), "dd/MM/yyyy", null);
                }
            }
            return result;
        }

        public static decimal? ConvertPriceInKhanhHoa(string input, char c, int index = 0)
        {
            decimal? result = (decimal?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains(".") || input.Contains("đ"))
                {
                    rsinput = input.Replace(".", "").Replace("đ", "").Trim();
                }
                else
                {
                    rsinput = input.Trim();
                }


                if (rsinput.Contains(c.ToString()))
                {
                    rsinput = SplitString(rsinput, c, index);
                    rsinput = String.Concat(rsinput, "000");
                    result = Convert.ToDecimal(rsinput);
                }
                else
                {
                    rsinput = String.Concat(rsinput, "000");
                    result = Convert.ToDecimal(rsinput);
                }
            }
            return result;
        }

        public static decimal? ConvertPriceInPhuYen(string input, char c, int index = 0, bool ischangeprice = false)
        {
            decimal? result = (decimal?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains("--"))
                {
                    input = input.Replace("--", "-").Trim();
                }
                if (input.Contains("."))
                {
                    input = input.Replace(".", "").Trim();
                }
                if (input.Contains(" "))
                {
                    input = input.Replace(" ", "").Trim();
                }
                if (ischangeprice == false && input.Contains(c.ToString()))
                {
                    rsinput = SplitString(input, c, index).Trim();
                }
                else if (ischangeprice == true && input.Contains("+") && input.Contains("-"))//change price is contains ± charactor
                {
                    rsinput = SplitString(input, c, index).Trim();
                }
                else if (input.Contains("±"))//change price is contains ± charactor
                {
                    rsinput = input.Replace("±", "-").Trim();
                }
                else
                {
                    rsinput = input.Trim();
                }

                if (rsinput != "0")
                {
                    result = !string.IsNullOrWhiteSpace(rsinput) ? Convert.ToDecimal(rsinput) : 0;
                }
                else
                {
                    result = 0;
                }

            }
            return result;
        }

        public static decimal? ConvertPriceInDaNang(string input)
        {
            decimal? result = (decimal?)null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains("-"))
                {
                    return result;
                }
                if (input.Contains("."))
                {
                    input = input.Replace(".", "").Trim();
                }
                if (input != "0")
                {
                    result = !string.IsNullOrWhiteSpace(input) ? Convert.ToDecimal(input) : 0;
                }
                else
                {
                    result = 0;
                }

            }
            return result;
        }

        public static decimal? ConvertPriceInDongThap(string input, char c, int index = 0)
        {
            decimal? result = (decimal?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.ToLower().Contains("không có cá vượt size"))
                {
                    return result;
                }
                else
                {
                    if (input.ToLower().Contains("đ/kg") || input.Contains("."))
                    {
                        rsinput = input.ToLower().Replace("đ/kg", "").Replace(".", "").Trim();
                    }
                    else
                    {
                        rsinput = input.Trim();
                    }

                    if (rsinput.Contains(c.ToString()))
                    {
                        rsinput = SplitString(rsinput, c, index);
                        result = Convert.ToDecimal(rsinput);
                    }
                    else
                    {
                        result = Convert.ToDecimal(rsinput);
                    }
                }
            }
            return result;
        }

        public static decimal? GetPriceSeafoodMarket(string rsinput, char c, int index = 0)
        {
            decimal? result = (decimal?)null;
            if (!string.IsNullOrWhiteSpace(rsinput))
            {
                if (rsinput.Contains("."))
                {
                    rsinput = rsinput.Replace(".", "");//If contains "." string convert to decimal lost last charactor
                }

                if (rsinput.Contains(c.ToString()))
                {
                    rsinput = SplitString(rsinput, c, index);

                    if (rsinput.Length < 3)
                    {
                        rsinput = String.Concat(rsinput, "000");
                    }
                    if (rsinput.Length < 4)
                    {
                        rsinput = String.Concat(rsinput, "00");
                    }
                    

                    result = Convert.ToDecimal(rsinput);
                }
                else
                {
                    result = Convert.ToDecimal(rsinput);
                }
            }
            return result;
        }

        public static DateTime? ReturnDateTimeAgroCoffee(string input)
        {
            DateTime? result = null;
            string[] arr = null;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains("-"))
                {
                    arr = input.Split('-');
                }
                if (input.Contains("/"))
                {
                    arr = input.Split('/');
                }

                var day = arr[0].Length < 2 ? string.Format("0{0}", arr[0]) : arr[0];
                var month = arr[1].Length < 2 ? string.Format("0{0}", arr[1]) : arr[1];
                result = DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, DateTime.Now.Year), "dd/MM/yyyy", null);
            }
            return result;
        }

        public static double? GetPriceAgroMonitorPepper(string rsinput, char c, int index = 0)
        {
            double? result = (double?)null;
            if (!string.IsNullOrWhiteSpace(rsinput))
            {
                if (rsinput.Contains(c.ToString()))
                {
                    rsinput = SplitString(rsinput, c, index);
                }

                if (rsinput.Length == 2)
                {
                    rsinput = String.Concat(rsinput, "000");
                }
                else if (rsinput.Length == 3)
                {
                    rsinput = String.Concat(rsinput, "00");
                }


                result = Convert.ToDouble(rsinput);
            }
            return result;
        }

        #region Banks
        public static bool IsNumeric(this string s)
        {
            if (s.Contains(","))
            {
                s = s.Replace(",", "").Trim();
            }
            if (s.Contains("."))
            {
                s = s.Replace(".", "").Trim();
            }
            if (s.Contains("&nbsp;"))
            {
                s = s.Replace("&nbsp;", "").Trim();
            }
            if (s.Contains("-"))
            {
                s = s.Replace("-", "").Trim();
            }
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static decimal? ConvertToMoney(string input)
        {
            decimal? result = (decimal?)null;
            var rsinput = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.Contains(","))
                {
                    input = input.Replace(",", "");
                }
                //if (input.Contains("."))
                //{
                //    input = input.Replace(".", "");
                //}
                if (input.Contains("&nbsp;"))
                {
                    input = input.Replace("&nbsp;", "").Trim();
                }
                if (input.Contains("-"))
                {
                    input = input.Replace("-", "").Trim();
                }
                result = !string.IsNullOrWhiteSpace(input) ? Convert.ToDecimal(input.Trim()) : (decimal?)null;
            }
            return result;
        }
        #endregion
    }
}
