using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Departments
{
    public record DepartmentResponseDto(int Id, string Code, string Name, DateTime UpdatedDate);
}
