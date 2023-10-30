using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Prueba.Shared
{
    public class SmartNavigationDTO
    {
        public SmartNavigationDTO(IEnumerable<ListItemDTO> items)
        {
            Lists = new List<ListItemDTO>(items);
        }

        public string Version { get; set; }
        public List<ListItemDTO> Lists { get; set; } = new List<ListItemDTO>();
    }
}
