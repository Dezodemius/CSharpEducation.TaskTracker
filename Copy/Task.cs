using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main.Copy
{
  public class Tasks : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; }

    public string AppUserId { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<Responsible> Responsibles { get; set; }
  }
}
