using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class clients
    {
        public clients()
        {
            clients_images = new HashSet<clients_images>();
            h_clients_incident = new HashSet<h_clients_incident>();
            h_dailylogs = new HashSet<h_dailylogs>();
            h_injuries = new HashSet<h_injuries>();
            h_medical_reminders = new HashSet<h_medical_reminders>();
            projects_clients = new HashSet<projects_clients>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string SafetyPlan { get; set; }
        public int Active { get; set; }
        public string State { get; set; }
        public long? IdfImg { get; set; }
        public string tmpMotherName { get; set; }
        public string tmpMotherInfo { get; set; }
        public string tmpFatherName { get; set; }
        public string tmpFatherInfo { get; set; }
        public string tmpAgencyWorker { get; set; }
        public string tmpAgencyWorkerInfo { get; set; }
        public string tmpAgency { get; set; }
        public string tmpAgencyInfo { get; set; }
        public string tmpPlacement { get; set; }
        public string tmpSupervisor { get; set; }
        public string tmpSpecialProgram { get; set; }
        public string tmpAdditionalInformation { get; set; }
        public string tmpSchool { get; set; }
        public string tmpSchoolInfo { get; set; }
        public string tmpTeacher { get; set; }
        public string tmpTeacherInfo { get; set; }
        public string tmpDoctorName { get; set; }
        public string tmpDoctorInfo { get; set; }
        public string tmpMedicationNotes { get; set; }

        public virtual ICollection<clients_images> clients_images { get; set; }
        public virtual ICollection<h_clients_incident> h_clients_incident { get; set; }
        public virtual ICollection<h_dailylogs> h_dailylogs { get; set; }
        public virtual ICollection<h_injuries> h_injuries { get; set; }
        public virtual ICollection<h_medical_reminders> h_medical_reminders { get; set; }
        public virtual ICollection<projects_clients> projects_clients { get; set; }
    }
}
