using System.Diagnostics;

namespace Display_incidents_by_technician
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            IncidentDB incidentDB = new IncidentDB();
            TechnicianDB technicianDB = new TechnicianDB();
            List<Incident> allIncidents = incidentDB.GetIncidents();
            List<Technician> allTechnicians = technicianDB.GetTechnicians();

            //linq to sort data
            var closedIncidentsByTechnician = allIncidents
                .Where(incident => incident.DateClosed != null)
                .OrderBy(incident => allTechnicians.FirstOrDefault(t => t.TechID == incident.TechID)?.Name)
                .ThenBy(incident => incident.DateOpened)
                .GroupBy(incident => incident.TechID);

            listBoxIncidents.Items.Clear();

            //header with fixed-width formatting
            listBoxIncidents.Items.Add("Technician          Product             Date Opened         Date Closed         Title");

            string previousTechnicianName = null;

            //looping through and adding proper formatting for display
            foreach (var technicianGroup in closedIncidentsByTechnician)
            {
                var firstIncident = technicianGroup.First();
                string technicianName = allTechnicians.FirstOrDefault(t => t.TechID == firstIncident.TechID)?.Name;

                if (previousTechnicianName == technicianName)
                {
                    technicianName = string.Empty;
                }

                foreach (var incident in technicianGroup)
                {
                    string formattedIncident = $"{technicianName,-19} {incident.ProductCode,-19} {incident.DateOpened.ToShortDateString(),-19} {incident.DateClosed?.ToShortDateString(),-19} {incident.Title}";
                    listBoxIncidents.Items.Add(formattedIncident);

                    previousTechnicianName = technicianName;
                    technicianName = string.Empty;
                }
            }
        }
    }

    class Incident
    {
        public int IncidentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public string CustomerID { get; set; }
        public string ProductCode { get; set; }
        public int? TechID { get; set; }

        public Incident(int id, string title, string description, DateTime dateOpened, DateTime? dateClosed, string customerID, string productCode, int? techID)
        {
            IncidentID = id;
            Title = title;
            Description = description;
            DateOpened = dateOpened;
            DateClosed = dateClosed;
            CustomerID = customerID;
            ProductCode = productCode;
            TechID = techID;
        }
    }


    class Technician
    {
        public int TechID { get; set; }
        public string Name { get; set; }

        public Technician(int id, string name)
        {
            TechID = id;
            Name = name;
        }
    }

    class IncidentDB
    {
        public List<Incident> GetIncidents()
        {
            List<Incident> incidents = new List<Incident>();

            //read data from Incidents.txt and populate the list
            string[] lines = File.ReadAllLines("Incidents.txt");
            foreach (string line in lines)
            {
                //included files use | to seperate fields
                string[] fields = line.Split('|');


                //checking for a few things here. I know i should have 8 fields
                if (fields.Length >= 8)
                {
                    int id;
                    if (int.TryParse(fields[0], out id))
                    {
                        //checking if data is a valid date
                        DateTime dateOpened;
                        if (DateTime.TryParse(fields[4], out dateOpened))
                        {
                            //checking for the closed date if its not empty
                            DateTime? dateClosed = null;
                            if (!string.IsNullOrEmpty(fields[5]))
                            {
                                DateTime parsedDateClosed;
                                if (DateTime.TryParse(fields[5], out parsedDateClosed))
                                {
                                    dateClosed = parsedDateClosed;
                                }
                            }

                            int techID;
                            if (int.TryParse(fields[3], out techID))
                            {
                                incidents.Add(new Incident(
                                    id,
                                    fields[6], //title
                                    fields[7], //description
                                    dateOpened,
                                    dateClosed,
                                    fields[1], //customer id
                                    fields[2], //product code
                                    techID
                                ));
                            }
                        }
                    }
                    else
                    {
                        //handl where parsing the IncidentID fails.
                    }
                }
                else
                {
                    //if not enough fields in the line.
                }
            }
            return incidents;
        }
    }



    class TechnicianDB
    {
        public List<Technician> GetTechnicians()
        {
            List<Technician> technicians = new List<Technician>();

            //read data from Technicians.txt
            string[] lines = File.ReadAllLines("Technicians.txt");
            foreach (string line in lines)
            {
                string[] fields = line.Split('|');

                if (fields.Length >= 2)
                {
                    int id;
                    if (int.TryParse(fields[0], out id))
                    {
                        technicians.Add(new Technician(id, fields[1]));
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            return technicians;
        }
    }
}