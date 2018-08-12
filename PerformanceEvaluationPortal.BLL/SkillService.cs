using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.Common.Mappers;

namespace PerformanceEvaluationPortal.BLL
{
    public class SkillService : ISkillService
    {
        private ApplicationDbContext _context;
        public SkillService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<SkillModel> GetAllSkills()
        {
            var skills = _context.Skills.ToList();
            var skillModel = skills.Select(x => x.MapSkillToSkillModel()).ToList();
            return skillModel;
        }
        //TO DO :Remove
        public SkillModel GetSkillById(int id)
        {
            var skill = _context.Skills.Where(x => x.SkillId == id).FirstOrDefault();
            return skill.MapSkillToSkillModel();
        }
    }
}
