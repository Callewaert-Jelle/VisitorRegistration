using System;
using System.Threading;
using System.Threading.Tasks;

namespace VisitorRegistration.Models.Domain
{
    public class Visitor
    {
        #region properties
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Entered { get; set; }
        public DateTime Left { get; set; }
        public string? Company { get; set; }
        public string? LicensePlate { get; set; }
        public VisitorType VisitorType { get; set; }
        public TimeSpan TimeInBuilding => Left.Subtract(Entered);
        #endregion

        #region constructors
        public Visitor()
        {
            Entered = DateTime.Now;
        }

        public Visitor(string name, string lastName, VisitorType type): this()
        {
            Name = name;
            LastName = lastName;
            VisitorType = type;
        }
        #endregion

        #region methods
        public void LogOut()
        {
            Left = DateTime.Now;
        }
        public int GetTotalHoursInBuilding()
        {
            return (int)TimeInBuilding.TotalHours;
        }
        #endregion
    }
}
