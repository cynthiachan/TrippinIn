C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



        }

        protected void lb_eat_Click(object sender, EventArgs e)
        {
            disableAllButtons();
            lb_eat.CssClass = "categoryButton eatActive";
            ViewState["Category"] = "Eat";

        }


        protected void lb_drink_Click(object sender, EventArgs e)
        {
            disableAllButtons();
            lb_drink.CssClass = "categoryButton drinkActive";
            ViewState["Category"] = "Drink";

        }

        protected void lb_party_Click(object sender, EventArgs e)
        {
            disableAllButtons();
            lb_party.CssClass = "categoryButton partyActive";
            ViewState["Category"] = "Party";

        }

        protected void lb_outdoor_Click(object sender, EventArgs e)
        {
            disableAllButtons();
            lb_outdoor.CssClass = "categoryButton outdoorActive";
            ViewState["Category"] = "Outdoor";
        }

        protected void lb_shopping_Click(object sender, EventArgs e)
        {
            disableAllButtons();
            lb_shopping.CssClass = "categoryButton shoppingActive";
            ViewState["Category"] = "Shopping";
        }

        private void disableAllButtons()
        {
            lb_eat.CssClass = "categoryButton eat";
            lb_drink.CssClass = "categoryButton drink";
            lb_party.CssClass = "categoryButton party";
            lb_outdoor.CssClass = "categoryButton outdoor";
            lb_shopping.CssClass = "categoryButton shopping";

        }

        public void LoadApiData()
        {
            string url = BuildUrl();
            bool success;
            string result = ScrapDataFromTheWeb(url, out success);

            if (success == true)
            {
                //lblshowresults.Text = result;
            }

            analyzeJson(result);
        }

        public void analyzeJson(string json)
        {
            JObject o = JObject.Parse(json);
            JArray Items = (JArray)o["response"]["data"];
            List<PlaceforView> li = new List<PlaceforView>();
            for (int i = 0; i < Items.Count; i++)
            {
                PlaceforView place = new PlaceforView();
                place.Title = Items[i]["title"].ToString();
                place.Grade = double.Parse(Items[i]["rating"].ToString());
                place.Subcategories = Items[i]["subcategory"].ToString();
                // to get the pictures we need to make Place call via api:
                // get trippin ID for the api call
                string trippininID = Items[i]["trippininid"].ToString();
                //build url for place 
                string url = "http://api.v1.trippinin.com/place/" + trippininID + "?key=" + GetKey();
                //scrap url
                bool success = false;
                string result = ScrapDataFromTheWeb(url, out success);

                if (success)
                {
                    JObject placeobject = JObject.Parse(result);
                    JArray Images = (JArray)placeobject["response"]["data"]["gallery"];
                    place.gallery = new List<string>();
                    for (int y = 0; y < Images.Count; y++)
                    {
                        string imgurl = Images[y]["url_small"].ToString();
                        place.gallery.Add(imgurl);
                    }

                }

                place.WereHere = int.Parse(Items[i]["totalwerehere"].ToString());
                place.HisRank = int.Parse(Items[i]["socialrankings"][0]["rank"].ToString());
                place.TotalRanks = int.Parse(Items[i]["socialrankings"][0]["total"].ToString());
                li.Add(place);
            }
            rpresults.DataSource = li;
            rpresults.DataBind();

        }

        public string BuildUrl()
        {
            string url = "http://api.v1.trippinin.com/City/";
            //Add City Name;
            url += dd1_city.SelectedItem.Text + "/";
            //Add main category;
            url += ViewState["Category"].ToString() + "/";
            //weekday
            url += ddl_day.SelectedItem.Text + "/";
            //time
            url += ddl_time.SelectedItem.Text + "?";
            //limit:
            url += "limit=25" + "&";
            //offset:
            url += "offset=0" + "&";
            // Add GetKey:
            url += "KEY=" + GetKey();
            //http://api.v1.trippinin.com/City/[city Name]/[main category]?day=[weekday]&time=[time]&limit=[limit]&offset=[offset]
            return url;
        }

        private string GetKey()
        {
           // return "confidential key";
        }


        public string ScrapDataFromTheWeb(string url, out bool success)
        {
            success = false;

            //Get URL Content as String            
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                // req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                req.Timeout = 3600000;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                //   forErrorString = response.ToString();
                // request.Timeout = 3600000;
                // Get the stream from the returned web response
                StreamReader stream = new StreamReader(res.GetResponseStream());
                // Get the stream from the returned web response
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string strLine;
                // Read the stream a line at a time and place each one
                // into the stringbuilder
                while ((strLine = stream.ReadLine()) != null)
                {
                    // Ignore blank lines
                    if (strLine.Length > 0)
                        sb.Append(strLine);
                }
                // Finished with the stream so close it now
                stream.Close();
                // Cache the streamed site now so it can be used
                // without reconnecting later
                // Thread.Sleep(1000);

                success = true;
                return sb.ToString();

            }
            catch (Exception ex)
            {

                //Console.WriteLine(url);
                //Thread.Sleep(2000);
                Console.WriteLine(ex.Message);
                return "error";

            }
        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            LoadApiData();
        }

        protected void ddl_day_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rpresults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var item = e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                // pull out the original data item
                PlaceforView _place = (PlaceforView)item.DataItem;
                // pull out the relevant repeater

                ((Label)item.FindControl("lbl_placeTitle")).Text = _place.Title;
                ((Label)item.FindControl("lbl_subcategories")).Text = _place.Subcategories;
                ((Label)item.FindControl("lbl_wereHere")).Text = _place.WereHere.ToString();
                ((Label)item.FindControl("lbl_grade")).Text = _place.Grade.ToString();
                ((Label)item.FindControl("lbl_totalRanks")).Text = _place.TotalRanks.ToString();

                if (_place.gallery.Count() > 0)
                {
                    ((Image)item.FindControl("imageLabel")).ImageUrl = _place.gallery[0];
                }
                else
                {
                   // ((Image)item.FindControl("imageLabel")).Visible = false;
                }  

            }
        }

        public class PlaceforView
        {
            public string Title { get; set; }
            public double Grade { get; set; }
            public string Subcategories { get; set; }
            public int WereHere { get; set; }
            public int HisRank { get; set; }
            public int TotalRanks { get; set; }
            public List<string> gallery { get; set; }
        }

    }
}
