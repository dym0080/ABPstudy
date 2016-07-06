using AutoMapper;
using ABPDemoNoZero.Tasks;
using ABPDemoNoZero.Tasks.Dtos;
using ABPDemoNoZero.Dictionarys;
using ABPDemoNoZero.Dictionarys.Dtos;

namespace ABPDemoNoZero
{
    internal static class DtoMappings
    {
        public static void Map()
        {
            //I specified mapping for AssignedPersonId since NHibernate does not fill Task.AssignedPersonId
            //If you will just use EF, then you can remove ForMember definition.
            Mapper.CreateMap<Task, TaskDto>().ForMember(t => t.AssignedPersonId, opts => opts.MapFrom(d => d.AssignedPerson.Id));
            Mapper.CreateMap<Dictionary, DictionaryDto>().ForMember(t => t.Code, opts => opts.MapFrom(d => d.Code));
        }
    }
}
