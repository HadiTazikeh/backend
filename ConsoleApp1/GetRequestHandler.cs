using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using ConsoleApp1;

namespace sepashttpserver
{
    class GetRequestHandler
    {
        public GetRequestHandler() { 
        
        }
        //public void process(HttpProcessor p)
        public void process(HttpListenerRequest request,HttpListenerResponse response)

            
        {
            string servicename = request.Url.LocalPath;
            Hashtable parameters = extractparameters(request.Url.Query);
            Object resualt=null;

            if (servicename.Equals("/allProduct") )
            {
                try
                {
                    resualt = DataBase.products;
                }
                catch (Exception ex) {
                  
                }
               printData(response, resualt);
            }
            else if (servicename.Equals("/newOrder") && parameters.Count == 3)
            {
                try
                {
                    String p_name = parameters["productName"].ToString();
                    int quantity = getInt(parameters["quantity"].ToString());
                    int express = getInt(parameters["express"].ToString());

                    Products product = null;

                    foreach (Products item in DataBase.products)
                    {
                        if (item.GetName().Equals(p_name))
                        {
                            product = item;
                            break;
                        }
                    }

                    if (product != null)
                    {
                        TimeRange result = product.deliveryTimes.Find(i => (quantity > i.Min && quantity <=i.Max));
                        DateTime orderTime = DateTime.Now;
                        int days = result.day;
                        if (express == 1 && days>1)
                        {
                            days = days - 1;
                        }
                        else if (express == 1 && days == 1)
                        {
                            days = 0;
                        }

                        DateTime deliverTime = AddBusinessDays(orderTime, days);

                        string delivery = "Delivery for "+product.GetName() +" on "+ orderTime.ToShortDateString() + " for "+ quantity + " " + product.GetUnit() +" should be "+ deliverTime.ToShortDateString();

                        printData(response, delivery);
                    }


                }
                catch (Exception ex)
                {

                }
                printData(response, resualt);
            }
            else
            {
                printData(response, resualt);
            }

        }

        public DateTime AddBusinessDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    dt = dt.AddDays(2);
            }
            return dt.AddDays(weeks * 7);
        }


        private int getInt(String num) {
            int ret = 0;
            try
            {
                ret = Int32.Parse(num);
            }catch(Exception ex){
            }
            return ret;
        }


        private Hashtable extractparameters(string requrl)
        {
            Hashtable parameters = new Hashtable();
            if (requrl != null && requrl.Length != 0)
            {
                int idx = requrl.IndexOf("?");
                if (idx > -1)
                {
                    string p1 = requrl.Substring(idx + 1);

                    while (p1 != null && p1.Length > 0)
                    {
                        int idx2 = p1.IndexOf("&");
                        if (idx2 > -1)
                        {
                            string p0 = p1.Substring(0, idx2);
                            string[] spstrs = p0.Split('=');
                            if (spstrs.Length == 2)
                            {
                                parameters.Add(spstrs[0], spstrs[1]);
                            }

                            p1 = p1.Substring(idx2 + 1);
                            continue;

                        }
                        else
                        {
                            string[] spstrs = p1.Split('=');
                            p1 = "";
                            if (spstrs.Length == 2)
                            {
                                parameters.Add(spstrs[0], spstrs[1]);
                            }
                        }
                    }

                }
            }
            return parameters;
        }

        public void printData(HttpListenerResponse response,Object obj)
        {

            if (obj != null)
            {
                byte[] encodedBytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                // byte[] tempres = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);
                System.IO.Stream output = response.OutputStream;
                output.Write(encodedBytes, 0, encodedBytes.Length);
                output.Close();
            }
        }

       
    }
}
