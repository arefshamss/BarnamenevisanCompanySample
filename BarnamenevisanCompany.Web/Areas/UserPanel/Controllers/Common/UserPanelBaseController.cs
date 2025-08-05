using BarnamenevisanCompany.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;

[Area("UserPanel")]
[Authorize]
public class UserPanelBaseController : BaseController;