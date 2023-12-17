using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM2_AppDev.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem>RoleList { get; set; }
    }
}
