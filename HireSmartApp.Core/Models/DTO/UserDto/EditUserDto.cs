using HireSmartApp.Core.Models.DTO.QuestionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Models.DTO.UserDto
{
    public class EditUserDto
    {
        public string Id {  get; set; }
        public GetQuestionDto QuestionDto { get; set; }
    }
}
