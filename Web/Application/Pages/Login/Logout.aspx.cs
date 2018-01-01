using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatrixPACS.ImageServer.Web.Common.Security;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Login
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SessionManager.SignOut();

			if (Request.Params["ReturnUrl"]!=null)
			{
				Response.Redirect(Request.Params["ReturnUrl"]);
			}
		}
	}
}