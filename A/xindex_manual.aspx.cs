using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class xindex_manual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack))
            {
                vagent_code.Value = Request.Form["agt"];
                vagent_name.Value = Request.Form["xgt"];

              //  YourHiddenField.Value = "hello";
                //rep_code.Text = Request.Form["agt"];
                //rep_xname.Text = Request.Form["xgt"];
                

            }

        }

        protected void Gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (select_merger_ass.SelectedItem.Value == "Assignment")
            //{

            //    d5.Visible = true;
            //    d4.Visible = false;
            //}

            //else
            //{
            //    d5.Visible = false;
            //    d4.Visible = true;
            //}
        }
    }
}