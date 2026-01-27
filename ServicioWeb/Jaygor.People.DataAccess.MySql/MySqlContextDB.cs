using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using JayGor.People.Entities.Entities;

namespace JayGor.People.DataAccess.MySql
{
    public partial class MySqlContextDB : DbContext
    {
        public MySqlContextDB(DbContextOptions<MySqlContextDB> options) : base(options) { }



        public virtual DbSet<chat_identity_users_timestamp> chat_identity_users_timestamp { get; set; }
        public virtual DbSet<chat_message_participant_state> chat_message_participant_state { get; set; }
        public virtual DbSet<chat_messages> chat_messages { get; set; }
        public virtual DbSet<chat_room_participants> chat_room_participants { get; set; }
        public virtual DbSet<chat_rooms> chat_rooms { get; set; }
        public virtual DbSet<clients> clients { get; set; }
        public virtual DbSet<client_forms> client_forms { get; set; }
        public virtual DbSet<client_form_fields> client_form_fields { get; set; }
        public virtual DbSet<client_form_values> client_form_values { get; set; }
        public virtual DbSet<client_form_reminders> client_form_reminders { get; set; }
        public virtual DbSet<client_form_reminder_users> client_form_reminder_users { get; set; }
        public virtual DbSet<client_form_image_values> client_form_image_values { get; set; }
        public virtual DbSet<client_form_field_values> client_form_field_values { get; set; }
        public virtual DbSet<clients_images> clients_images { get; set; }
        public virtual DbSet<common_errors> common_errors { get; set; }
        public virtual DbSet<duplicate_scheduling> duplicate_scheduling { get; set; }
        public virtual DbSet<duplicate_tasks> duplicate_tasks { get; set; }
        public virtual DbSet<form_fields> form_fields { get; set; }
        public virtual DbSet<h_catalog> h_catalog { get; set; }
        public virtual DbSet<h_clients_incident> h_clients_incident { get; set; }
        public virtual DbSet<h_dailylog_involved_people> h_dailylog_involved_people { get; set; }
        public virtual DbSet<h_dailylogs> h_dailylogs { get; set; }
        public virtual DbSet<h_degree_of_injury> h_degree_of_injury { get; set; }
        public virtual DbSet<h_incident_involved_people> h_incident_involved_people { get; set; }
        public virtual DbSet<h_incident_values> h_incident_values { get; set; }
        public virtual DbSet<h_incidents> h_incidents { get; set; }
        public virtual DbSet<h_injuries> h_injuries { get; set; }
        public virtual DbSet<h_injury_values> h_injury_values { get; set; }
        public virtual DbSet<h_medical_reminders> h_medical_reminders { get; set; }
        public virtual DbSet<h_ministeries> h_ministeries { get; set; }
        public virtual DbSet<h_region> h_region { get; set; }
        public virtual DbSet<h_type_serious_occurrence> h_type_serious_occurrence { get; set; }
        public virtual DbSet<h_umab_intervention> h_umab_intervention { get; set; }
        public virtual DbSet<identity_images> identity_images { get; set; }
        public virtual DbSet<identity_roles> identity_roles { get; set; }
        public virtual DbSet<identity_users> identity_users { get; set; }
        public virtual DbSet<identity_users_rol> identity_users_rol { get; set; }
        public virtual DbSet<meetings> meetings { get; set; }
        public virtual DbSet<periods> periods { get; set; }
        public virtual DbSet<positions> positions { get; set; }
        public virtual DbSet<project_assets> project_assets { get; set; }
        public virtual DbSet<project_owners> project_owners { get; set; }
        public virtual DbSet<project_petty_cash> project_petty_cash { get; set; }
        public virtual DbSet<project_pettycash_categories> project_pettycash_categories { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<projects_clients> projects_clients { get; set; }
        public virtual DbSet<project_forms> project_forms { get; set; }
        public virtual DbSet<project_form_fields> project_form_fields { get; set; }
        public virtual DbSet<project_form_values> project_form_values { get; set; }
        public virtual DbSet<project_form_reminders> project_form_reminders { get; set; }
        public virtual DbSet<project_form_reminder_users> project_form_reminder_users { get; set; }
        public virtual DbSet<project_form_image_values> project_form_image_values { get; set; }
        public virtual DbSet<project_form_field_values> project_form_field_values { get; set; }
        public virtual DbSet<reminders> reminders { get; set; }
        public virtual DbSet<scheduling> scheduling { get; set; }
        public virtual DbSet<settings_reminder_time> settings_reminder_time { get; set; }
        public virtual DbSet<staff> staff { get; set; }
        public virtual DbSet<staff_forms> staff_forms { get; set; }
        public virtual DbSet<staff_form_fields> staff_form_fields { get; set; }
        public virtual DbSet<staff_form_values> staff_form_values { get; set; }
        public virtual DbSet<staff_form_reminders> staff_form_reminders { get; set; }
        public virtual DbSet<staff_form_reminder_users> staff_form_reminder_users { get; set; }
        public virtual DbSet<staff_form_image_values> staff_form_image_values { get; set; }
        public virtual DbSet<staff_form_field_values> staff_form_field_values { get; set; }
        public virtual DbSet<staff_period_settings> staff_period_settings { get; set; }
        public virtual DbSet<staff_project_position> staff_project_position { get; set; }
        public virtual DbSet<statuses> statuses { get; set; }
        public virtual DbSet<tasks> tasks { get; set; }
        public virtual DbSet<tasks_reminders> tasks_reminders { get; set; }
        public virtual DbSet<tasks_state_history> tasks_state_history { get; set; }
        public virtual DbSet<time_tracking> time_tracking { get; set; }
        public virtual DbSet<time_tracking_auto> time_tracking_auto { get; set; }
        public virtual DbSet<time_tracking_review> time_tracking_review { get; set; }
        public virtual DbSet<todo_items> todo_items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var serverVersion = new MySqlServerVersion(new Version(5, 7, 19));
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;userid=root;port=3306;database=people.prod;sslmode=none;", serverVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chat_identity_users_timestamp>(entity =>
            {
                entity.HasIndex(e => e.IdfIdentityUser)
                    .HasName("fk_chatIdentuserstime_Identity_Users_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.ClientMessagesVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ClientParticipantsVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ClientRoomVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DatePushSent)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IdfIdentityUser).HasColumnType("bigint(10)");

                entity.Property(e => e.ServerMessagesVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ServerParticipantsVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ServerRoomVersion)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdfIdentityUserNavigation)
                    .WithMany(p => p.chat_identity_users_timestamp)
                    .HasForeignKey(d => d.IdfIdentityUser)
                    .HasConstraintName("fk_chatIdentuserstime_Identity_Users");
            });

            modelBuilder.Entity<chat_message_participant_state>(entity =>
            {
                entity.HasIndex(e => e.IdfMessage)
                    .HasName("fk_chatmsgpartstate_chatmessages_idx");

                entity.HasIndex(e => e.IdfParticipant)
                    .HasName("fk_chatmsgpartstate_part_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Delivered)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfMessage).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfParticipant).HasColumnType("bigint(10)");

                entity.Property(e => e.Read)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdfMessageNavigation)
                    .WithMany(p => p.chat_message_participant_state)
                    .HasForeignKey(d => d.IdfMessage)
                    .HasConstraintName("fk_chatmsgpartstate_chatmessages");

                entity.HasOne(d => d.IdfParticipantNavigation)
                    .WithMany(p => p.chat_message_participant_state)
                    .HasForeignKey(d => d.IdfParticipant)
                    .HasConstraintName("fk_chatmsgpartstate_part");
            });

            modelBuilder.Entity<chat_messages>(entity =>
            {
                entity.HasIndex(e => e.IdfChatRoom)
                    .HasName("fk_chatmessages_chatroom_idx");

                entity.HasIndex(e => e.IdfIdentityUserSender)
                    .HasName("fk_chat_messages_identityusers_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IdfChatRoom).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfIdentityUserSender).HasColumnType("bigint(10)");

                entity.Property(e => e.Messages)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.IdfChatRoomNavigation)
                    .WithMany(p => p.chat_messages)
                    .HasForeignKey(d => d.IdfChatRoom)
                    .HasConstraintName("fk_chatmessages_chatroom");

                entity.HasOne(d => d.IdfIdentityUserSenderNavigation)
                    .WithMany(p => p.chat_messages)
                    .HasForeignKey(d => d.IdfIdentityUserSender)
                    .HasConstraintName("fk_chat_messages_identityusers");
            });

            modelBuilder.Entity<chat_room_participants>(entity =>
            {
                entity.HasIndex(e => e.IdfChatRoom)
                    .HasName("fk_chatroompart_chat_room_idx");

                entity.HasIndex(e => e.IdfIdentityUser)
                    .HasName("fk_chatroompart_identityuser_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.IdfChatRoom).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfIdentityUser).HasColumnType("bigint(10)");

                entity.HasOne(d => d.IdfChatRoomNavigation)
                    .WithMany(p => p.chat_room_participants)
                    .HasForeignKey(d => d.IdfChatRoom)
                    .HasConstraintName("fk_chatroompart_chat_room");

                entity.HasOne(d => d.IdfIdentityUserNavigation)
                    .WithMany(p => p.chat_room_participants)
                    .HasForeignKey(d => d.IdfIdentityUser)
                    .HasConstraintName("fk_chatroompart_identityuser");
            });

            modelBuilder.Entity<chat_rooms>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<clients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Active).HasColumnType("int(11)");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasColumnType("varchar(150)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.IdfImg).HasColumnType("bigint(10)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Notes).HasColumnType("varchar(500)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(50)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.tmpAgency).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpAgencyInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpAgencyWorker).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpAgencyWorkerInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpDoctorInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpDoctorName).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpFatherInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpFatherName).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpMedicationNotes).HasColumnType("longtext");

                entity.Property(e => e.tmpMotherInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpMotherName).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpPlacement).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpSchool).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpSchoolInfo).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpSpecialProgram).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpSupervisor).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpTeacher).HasColumnType("varchar(256)");

                entity.Property(e => e.tmpTeacherInfo).HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<clients_images>(entity =>
            {
                entity.HasIndex(e => e.IdfClient)
                    .HasName("fk_clients_images_clients_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfClient).HasColumnType("bigint(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdfClientNavigation)
                    .WithMany(p => p.clients_images)
                    .HasForeignKey(d => d.IdfClient)
                    .HasConstraintName("fk_clients_images_clients");
            });

            modelBuilder.Entity<common_errors>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.IdfUser).HasColumnType("bigint(10)");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<duplicate_scheduling>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.DuplicateValue)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.EndAfter)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Monthly_Day).HasColumnType("int(11)");

                entity.Property(e => e.RepeatEvery)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Weekly_Fr).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Mo).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Sa).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Su).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Th).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Tu).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_We).HasColumnType("int(11)");

                entity.Property(e => e.Yearly_Month).HasColumnType("int(11)");

                entity.Property(e => e.Yearly_MonthDay).HasColumnType("int(11)");
            });

            modelBuilder.Entity<duplicate_tasks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.DuplicateValue)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.EndAfter)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Monthly_Day).HasColumnType("int(11)");

                entity.Property(e => e.RepeatEvery)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Weekly_Fr).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Mo).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Sa).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Su).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Th).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_Tu).HasColumnType("int(11)");

                entity.Property(e => e.Weekly_We).HasColumnType("int(11)");

                entity.Property(e => e.Yearly_Month).HasColumnType("int(11)");

                entity.Property(e => e.Yearly_MonthDay).HasColumnType("int(11)");
            });

            modelBuilder.Entity<h_catalog>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("varchar(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(125)");

                entity.Property(e => e.IdentifierGroup)
                    .IsRequired()
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(2)");
            });

            modelBuilder.Entity<h_clients_incident>(entity =>
            {
                entity.HasIndex(e => e.IdfClient)
                    .HasName("h_clients_incident_client_idx");

                entity.HasIndex(e => e.IdfIncident)
                    .HasName("h_clients_incident_incident_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfClient).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfIncident).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfClientNavigation)
                    .WithMany(p => p.h_clients_incident)
                    .HasForeignKey(d => d.IdfClient)
                    .HasConstraintName("h_clients_incident_client");

                entity.HasOne(d => d.IdfIncidentNavigation)
                    .WithMany(p => p.h_clients_incident)
                    .HasForeignKey(d => d.IdfIncident)
                    .HasConstraintName("h_clients_incident_incident");
            });

            modelBuilder.Entity<h_dailylog_involved_people>(entity =>
            {
                entity.HasIndex(e => e.IdfDailyLog)
                    .HasName("h_dailylog_involved_people_dailyLog_idx");

                entity.HasIndex(e => e.IdfSPP)
                    .HasName("h_dailylog_involved_people_spp_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdentifierGroup)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.IdfDailyLog).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfSPP).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfDailyLogNavigation)
                    .WithMany(p => p.h_dailylog_involved_people)
                    .HasForeignKey(d => d.IdfDailyLog)
                    .HasConstraintName("h_dailylog_involved_people_dailyLog");

                entity.HasOne(d => d.IdfSPPNavigation)
                    .WithMany(p => p.h_dailylog_involved_people)
                    .HasForeignKey(d => d.IdfSPP)
                    .HasConstraintName("h_dailylog_involved_people_spp");
            });

            modelBuilder.Entity<h_dailylogs>(entity =>
            {
                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_h_dailylogs_clients_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_h_dailylogs_periods_idx");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("fk_h_dailylogs_projects_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Attended).HasColumnType("varchar(1000)");

                entity.Property(e => e.ClientId).HasColumnType("bigint(10)");

                entity.Property(e => e.Comments).HasColumnType("varchar(1000)");

                entity.Property(e => e.ContactFamily).HasColumnType("varchar(1000)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.GeneralMood).HasColumnType("varchar(1000)");

                entity.Property(e => e.Health).HasColumnType("varchar(1000)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.InHouseProg).HasColumnType("varchar(1000)");

                entity.Property(e => e.InteractionPeers).HasColumnType("varchar(1000)");

                entity.Property(e => e.InteractionStaff).HasColumnType("varchar(1000)");

                entity.Property(e => e.Other)
                    .HasColumnType("varchar(1000)")
                    .HasDefaultValueSql("' '");

                entity.Property(e => e.Placement)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.ProjectId).HasColumnType("bigint(10)");

                entity.Property(e => e.School).HasColumnType("varchar(1000)");

                entity.Property(e => e.SeriousOccurrence)
                    .HasColumnType("varchar(1000)")
                    .HasDefaultValueSql("' '");

                entity.Property(e => e.StaffOnShift)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.UserId).HasColumnType("bigint(10)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.h_dailylogs)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_h_dailylogs_clients");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.h_dailylogs)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_h_dailylogs_periods");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.h_dailylogs)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_h_dailylogs_projects");
            });

            modelBuilder.Entity<h_degree_of_injury>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<h_incident_involved_people>(entity =>
            {
                entity.HasIndex(e => e.IdfSPP)
                    .HasName("h_incident_involved_people_spp_idx");

                entity.HasIndex(e => e.idfIncident)
                    .HasName("h_incident_involved_people_incident_idx");

                entity.Property(e => e.id).HasColumnType("bigint(19)");

                entity.Property(e => e.IdentifierGroup)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.IdfSPP).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.idfIncident).HasColumnType("bigint(10)");

                entity.HasOne(d => d.IdfSPPNavigation)
                    .WithMany(p => p.h_incident_involved_people)
                    .HasForeignKey(d => d.IdfSPP)
                    .HasConstraintName("h_incident_involved_people_spp");

                entity.HasOne(d => d.idfIncidentNavigation)
                    .WithMany(p => p.h_incident_involved_people)
                    .HasForeignKey(d => d.idfIncident)
                    .HasConstraintName("h_incident_involved_people_incident");
            });

            modelBuilder.Entity<h_incident_values>(entity =>
            {
                entity.HasIndex(e => e.idfCatalog)
                    .HasName("h_incident_values_catalog_idx");

                entity.HasIndex(e => e.idfIncident)
                    .HasName("h_incident_values_incident_idx");

                entity.Property(e => e.id).HasColumnType("bigint(10)");

                entity.Property(e => e.Value).HasColumnType("varchar(1000)");

                entity.Property(e => e.idfCatalog)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.idfIncident).HasColumnType("bigint(10)");

                entity.HasOne(d => d.idfCatalogNavigation)
                    .WithMany(p => p.h_incident_values)
                    .HasForeignKey(d => d.idfCatalog)
                    .HasConstraintName("h_incident_values_catalog");

                entity.HasOne(d => d.idfIncidentNavigation)
                    .WithMany(p => p.h_incident_values)
                    .HasForeignKey(d => d.idfIncident)
                    .HasConstraintName("h_incident_values_incident");
            });

            modelBuilder.Entity<h_incidents>(entity =>
            {
                entity.HasIndex(e => e.IdfMinistry)
                    .HasName("h_incidents_ministry_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("h_incidents_periods_idx");

                entity.HasIndex(e => e.IdfRegion)
                    .HasName("h_incidents_region_idx");

                entity.HasIndex(e => e.IdfTypeOfSeriousOccurrence)
                    .HasName("h_incidents_typeofso_idx");

                entity.Property(e => e.id).HasColumnType("bigint(10)");

                entity.Property(e => e.DateIncident).HasColumnType("datetime");

                entity.Property(e => e.DateTimeWhenSeriousOccurrence).HasColumnType("datetime");

                entity.Property(e => e.DescName)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.IdfMinistry).HasColumnType("int(11)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfRegion).HasColumnType("int(11)");

                entity.Property(e => e.IdfTypeOfSeriousOccurrence)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfUmabIntervention).HasColumnType("int(11)");

                entity.Property(e => e.IsSeriousOcurrence)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SentToMinistry)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.TimeIncident).HasColumnType("time");

                entity.HasOne(d => d.IdfMinistryNavigation)
                    .WithMany(p => p.h_incidents)
                    .HasForeignKey(d => d.IdfMinistry)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("h_incidents_ministry");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.h_incidents)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("h_incidents_periods");

                entity.HasOne(d => d.IdfRegionNavigation)
                    .WithMany(p => p.h_incidents)
                    .HasForeignKey(d => d.IdfRegion)
                    .HasConstraintName("h_incidents_region");

                entity.HasOne(d => d.IdfTypeOfSeriousOccurrenceNavigation)
                    .WithMany(p => p.h_incidents)
                    .HasForeignKey(d => d.IdfTypeOfSeriousOccurrence)
                    .HasConstraintName("h_incidents_typeofso");
            });

            modelBuilder.Entity<h_injuries>(entity =>
            {
                entity.HasIndex(e => e.IdfClient)
                    .HasName("h_injuries_client_idx");

                entity.HasIndex(e => e.IdfDegreeOfInjury)
                    .HasName("h_injuries_degree_idx");

                entity.HasIndex(e => e.IdfIncident)
                    .HasName("h_injuries_incident_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("h_injuries_period_idx");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("h_injuries_projects_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.BodySerialized).HasColumnType("longtext");

                entity.Property(e => e.DateOfInjury)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DateReportedSupervisor)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DescName)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdfClient).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfDegreeOfInjury).HasColumnType("int(11)");

                entity.Property(e => e.IdfIncident).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfSupervisor).HasColumnType("bigint(10)");

                entity.Property(e => e.ProjectId).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfClientNavigation)
                    .WithMany(p => p.h_injuries)
                    .HasForeignKey(d => d.IdfClient)
                    .HasConstraintName("h_injuries_client");

                entity.HasOne(d => d.IdfDegreeOfInjuryNavigation)
                    .WithMany(p => p.h_injuries)
                    .HasForeignKey(d => d.IdfDegreeOfInjury)
                    .HasConstraintName("h_injuries_degree");

                entity.HasOne(d => d.IdfIncidentNavigation)
                    .WithMany(p => p.h_injuries)
                    .HasForeignKey(d => d.IdfIncident)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("h_injuries_incident");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.h_injuries)
                    .HasForeignKey(d => d.IdfPeriod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("h_injuries_period");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.h_injuries)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("h_injuries_projects");
            });

            modelBuilder.Entity<h_injury_values>(entity =>
            {
                entity.HasIndex(e => e.idfCatalog)
                    .HasName("h_incident_values_catalog_idx");

                entity.HasIndex(e => e.idfInjury)
                    .HasName("h_injury_values_idx");

                entity.Property(e => e.id).HasColumnType("bigint(10)");

                entity.Property(e => e.Value).HasColumnType("varchar(1000)");

                entity.Property(e => e.idfCatalog)
                    .IsRequired()
                    .HasColumnType("varchar(7)");

                entity.Property(e => e.idfInjury).HasColumnType("bigint(10)");

                entity.HasOne(d => d.idfCatalogNavigation)
                    .WithMany(p => p.h_injury_values)
                    .HasForeignKey(d => d.idfCatalog)
                    .HasConstraintName("h_injury_values_cat");

                entity.HasOne(d => d.idfInjuryNavigation)
                    .WithMany(p => p.h_injury_values)
                    .HasForeignKey(d => d.idfInjury)
                    .HasConstraintName("h_injury_values_injuries");
            });

            modelBuilder.Entity<h_medical_reminders>(entity =>
            {
                entity.HasIndex(e => e.IdfAssignedTo)
                    .HasName("fk_h_medical_reminders_spp_idx");

                entity.HasIndex(e => e.IdfClient)
                    .HasName("fk_h_medical_reminders_client_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Datetime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.IdfAssignedTo).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfClient).HasColumnType("bigint(10)");

                entity.Property(e => e.Reminder)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.HasOne(d => d.IdfAssignedToNavigation)
                    .WithMany(p => p.h_medical_reminders)
                    .HasForeignKey(d => d.IdfAssignedTo)
                    .HasConstraintName("fk_h_medical_reminders_spp");

                entity.HasOne(d => d.IdfClientNavigation)
                    .WithMany(p => p.h_medical_reminders)
                    .HasForeignKey(d => d.IdfClient)
                    .HasConstraintName("fk_h_medical_reminders_client");
            });

            modelBuilder.Entity<h_ministeries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<h_region>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<h_type_serious_occurrence>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<h_umab_intervention>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<identity_images>(entity =>
            {
                entity.HasIndex(e => e.IdfIdentity_user)
                    .HasName("fk_identityImages_identityUsers_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfIdentity_user).HasColumnType("bigint(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdfIdentity_userNavigation)
                    .WithMany(p => p.identity_images)
                    .HasForeignKey(d => d.IdfIdentity_user)
                    .HasConstraintName("fk_identityImages_identityUsers");
            });

            modelBuilder.Entity<identity_roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.DisplayShortName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RolDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<identity_users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UserName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("Id");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Face).HasColumnType("longtext");

                entity.Property(e => e.FaceStamp)
                    .IsRequired()
                    .HasColumnType("varchar(125)")
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.FirstName).HasColumnType("varchar(45)");

                entity.Property(e => e.GeoTrackingEvery)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdOneSignal).HasColumnType("varchar(256)");

                entity.Property(e => e.IdOneSignalWeb).HasColumnType("varchar(256)");

                entity.Property(e => e.IdfImg).HasColumnType("bigint(10)");

                entity.Property(e => e.LastName).HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<identity_users_rol>(entity =>
            {
                entity.HasIndex(e => e.IdfRol)
                    .HasName("IdentityUsersRolIdfRol_Identity_Roles_Id_idx");

                entity.HasIndex(e => e.IdfUser)
                    .HasName("_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfRol).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfUser).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.HasOne(d => d.IdfRolNavigation)
                    .WithMany(p => p.identity_users_rol)
                    .HasForeignKey(d => d.IdfRol)
                    .HasConstraintName("IdentityUsersRolIdfRol_Identity_Roles_Id");

                entity.HasOne(d => d.IdfUserNavigation)
                    .WithMany(p => p.identity_users_rol)
                    .HasForeignKey(d => d.IdfUser)
                    .HasConstraintName("IdentityUsersRolIdfUser_Identity_Users_Id");
            });

            modelBuilder.Entity<meetings>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Active).HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.OptionalAtt)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Organizers)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Remind).HasColumnType("int(11)");

                entity.Property(e => e.RequiredAtt)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.UserID).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<periods>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description).HasColumnType("varchar(256)");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.IdfCreatedBy)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.State).HasColumnType("varchar(2)");

                entity.Property(e => e.To).HasColumnType("datetime");
            });

            modelBuilder.Entity<positions>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.State).HasColumnType("varchar(2)");
            });

            modelBuilder.Entity<project_assets>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Category).HasColumnType("varchar(250)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(1000)");

                entity.Property(e => e.ProjectId).HasColumnType("bigint(20)");

                entity.Property(e => e.TaxFed).HasColumnType("int(11)");

                entity.Property(e => e.TaxProb).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<project_owners>(entity =>
            {
                entity.HasIndex(e => e.IdfOwner)
                    .HasName("ProjectOwnerStaff_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_project:owners_periods_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("ProjectOwnerProject_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfOwner).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdfOwnerNavigation)
                    .WithMany(p => p.project_owners)
                    .HasForeignKey(d => d.IdfOwner)
                    .HasConstraintName("ProjectOwnerStaff");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.project_owners)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_project:owners_periods");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.project_owners)
                    .HasForeignKey(d => d.IdfProject)
                    .HasConstraintName("ProjectOwnerProject");
            });

            modelBuilder.Entity<project_petty_cash>(entity =>
            {
                entity.HasIndex(e => e.IdfCategories)
                    .HasName("fk_ppc_ppcc_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_project_petty_cash_periods_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("fk_ project_petty_cash_projects_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.IdfCategories).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(10)");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfCategoriesNavigation)
                    .WithMany(p => p.project_petty_cash)
                    .HasForeignKey(d => d.IdfCategories)
                    .HasConstraintName("fk_ppc_ppcc");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.project_petty_cash)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_project_petty_cash_periods");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.project_petty_cash)
                    .HasForeignKey(d => d.IdfProject)
                    .HasConstraintName("fk_ project_petty_cash_projects");
            });

            modelBuilder.Entity<project_pettycash_categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<projects>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Address).HasColumnType("varchar(256)");

                entity.Property(e => e.City).HasColumnType("varchar(125)");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'#5bc0de'");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description).HasColumnType("varchar(500)");

                entity.Property(e => e.Phone1).HasColumnType("varchar(45)");

                entity.Property(e => e.Phone2).HasColumnType("varchar(45)");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.Visible)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<projects_clients>(entity =>
            {
                entity.HasIndex(e => e.IdfClient)
                    .HasName("fk_projects_clients_clients_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_projects_clients_periods_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("fk_projects_clients_projects_idx");

                entity.HasIndex(e => e.IdfSPP)
                    .HasName("fk_projects_clients_spp_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfClient).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'113'");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfSPP).HasColumnType("bigint(10)");

                entity.Property(e => e.State).HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdfClientNavigation)
                    .WithMany(p => p.projects_clients)
                    .HasForeignKey(d => d.IdfClient)
                    .HasConstraintName("fk_projects_clients_clients");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.projects_clients)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_projects_clients_periods");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.projects_clients)
                    .HasForeignKey(d => d.IdfProject)
                    .HasConstraintName("fk_projects_clients_projects");

                entity.HasOne(d => d.IdfSPPNavigation)
                    .WithMany(p => p.projects_clients)
                    .HasForeignKey(d => d.IdfSPP)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_projects_clients_spp");
            });

            modelBuilder.Entity<reminders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Active).HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateRemind).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<scheduling>(entity =>
            {
                entity.HasIndex(e => e.IdDuplicate)
                    .HasName("fk_scheduling_duplicate_scheduling_idx");

                entity.HasIndex(e => e.IdfAssignedTo)
                    .HasName("SchedulingStaffProjectPosition_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("SchedulingPeriod_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("SchedulingProject_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.AllDay).HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.IdDuplicate).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfAssignedTo)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfCreatedBy)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(10)");

                entity.Property(e => e.State).HasColumnType("varchar(2)");

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.HasOne(d => d.IdDuplicateNavigation)
                    .WithMany(p => p.scheduling)
                    .HasForeignKey(d => d.IdDuplicate)
                    .HasConstraintName("fk_scheduling_duplicate_scheduling");

                entity.HasOne(d => d.IdfAssignedToNavigation)
                    .WithMany(p => p.scheduling)
                    .HasForeignKey(d => d.IdfAssignedTo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SchedulingStaffProjectPosition");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.scheduling)
                    .HasForeignKey(d => d.IdfPeriod)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SchedulingPeriod");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.scheduling)
                    .HasForeignKey(d => d.IdfProject)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SchedulingProject");
            });

            modelBuilder.Entity<settings_reminder_time>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.MinutesBefore).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasIndex(e => e.IdfUser)
                    .HasName("StaffUser_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.AvailableForManyPrograms)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CellNumber).HasColumnType("varchar(45)");

                entity.Property(e => e.City).HasColumnType("varchar(125)");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'#5bc0de'");

                entity.Property(e => e.EmergencyPerson).HasColumnType("varchar(128)");

                entity.Property(e => e.EmergencyPersonInfo).HasColumnType("varchar(128)");

                entity.Property(e => e.HealthInsuranceNumber).HasColumnType("varchar(128)");

                entity.Property(e => e.HomeAddress).HasColumnType("varchar(256)");

                entity.Property(e => e.HomePhone).HasColumnType("varchar(128)");

                entity.Property(e => e.IdfUser).HasColumnType("bigint(10)");

                entity.Property(e => e.SocialInsuranceNumber).HasColumnType("varchar(128)");

                entity.Property(e => e.SpouceName).HasColumnType("varchar(128)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.Property(e => e.WorkStartDate).HasColumnType("datetime");

                entity.Property(e => e.tmpAccreditations).HasColumnType("longtext");

                entity.HasOne(d => d.IdfUserNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.IdfUser)
                    .HasConstraintName("fk_StaffUser");
            });

            modelBuilder.Entity<staff_period_settings>(entity =>
            {
                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_staff_period_sett_period_idx");

                entity.HasIndex(e => e.IdfStaff)
                    .HasName("fk_staff_period_sett_staff_idx");

                entity.HasIndex(e => new { e.IdfPeriod, e.IdfStaff })
                    .HasName("fk_staff_period_sett_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfStaff).HasColumnType("bigint(20)");

                entity.Property(e => e.WorkingHours).HasColumnType("int(11)");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.staff_period_settings)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_staff_period_sett_period");

                entity.HasOne(d => d.IdfStaffNavigation)
                    .WithMany(p => p.staff_period_settings)
                    .HasForeignKey(d => d.IdfStaff)
                    .HasConstraintName("fk_staff_period_sett_staff");
            });

            modelBuilder.Entity<staff_project_position>(entity =>
            {
                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_staff_project_position_periods_idx");

                entity.HasIndex(e => e.IdfPosition)
                    .HasName("SPPPosition_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("SPPProject_idx");

                entity.HasIndex(e => e.IdfStaff)
                    .HasName("SPPStaff_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Hours)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPosition).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfStaff).HasColumnType("bigint(20)");

                entity.Property(e => e.State).HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.staff_project_position)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_staff_project_position_periods");

                entity.HasOne(d => d.IdfPositionNavigation)
                    .WithMany(p => p.staff_project_position)
                    .HasForeignKey(d => d.IdfPosition)
                    .HasConstraintName("SPPPosition");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.staff_project_position)
                    .HasForeignKey(d => d.IdfProject)
                    .HasConstraintName("SPPProject");

                entity.HasOne(d => d.IdfStaffNavigation)
                    .WithMany(p => p.staff_project_position)
                    .HasForeignKey(d => d.IdfStaff)
                    .HasConstraintName("SPPStaff");
            });

            modelBuilder.Entity<statuses>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<tasks>(entity =>
            {
                entity.HasIndex(e => e.IdfAssignableRol)
                    .HasName("fk_tasks_positions_idx");

                entity.HasIndex(e => e.IdfAssignedTo)
                    .HasName("TaskSPP_idx");

                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("TaskPeriod_idx");

                entity.HasIndex(e => e.IdfProject)
                    .HasName("TaskProject_idx");

                entity.HasIndex(e => e.IdfStatus)
                    .HasName("TaskStatus");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.Address).HasColumnType("varchar(100)");

                entity.Property(e => e.AllDay).HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(256)");

                entity.Property(e => e.Hours)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdDuplicate).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfAssignableRol).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfAssignedTo).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfCreatedBy)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfProject).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfStatus).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfTaskParent)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Lat).HasColumnType("varchar(60)");

                entity.Property(e => e.Lon).HasColumnType("varchar(60)");

                entity.Property(e => e.Notes).HasColumnType("varchar(15000)");

                entity.Property(e => e.RecurrenceException).HasColumnType("varchar(256)");

                entity.Property(e => e.RecurrencePattern).HasColumnType("varchar(256)");

                entity.Property(e => e.State).HasColumnType("varchar(2)");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'RQ'");

                entity.HasOne(d => d.IdfAssignableRolNavigation)
                    .WithMany(p => p.tasks)
                    .HasForeignKey(d => d.IdfAssignableRol)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_tasks_positions");

                entity.HasOne(d => d.IdfAssignedToNavigation)
                    .WithMany(p => p.tasks)
                    .HasForeignKey(d => d.IdfAssignedTo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_tasks_spp");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.tasks)
                    .HasForeignKey(d => d.IdfPeriod)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TaskPeriod");

                entity.HasOne(d => d.IdfProjectNavigation)
                    .WithMany(p => p.tasks)
                    .HasForeignKey(d => d.IdfProject)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TaskProject");

                entity.HasOne(d => d.IdfStatusNavigation)
                    .WithMany(p => p.tasks)
                    .HasForeignKey(d => d.IdfStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tasks_ibfk_1");
            });

            modelBuilder.Entity<tasks_reminders>(entity =>
            {
                entity.HasIndex(e => e.IdfSettingReminderTime)
                    .HasName("fk_tasks_reminders_settingReminder_idx");

                entity.HasIndex(e => e.IdfTask)
                    .HasName("fk_tasks_reminders_tasks_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod)
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdfSettingReminderTime).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfTask).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfSettingReminderTimeNavigation)
                    .WithMany(p => p.tasks_reminders)
                    .HasForeignKey(d => d.IdfSettingReminderTime)
                    .HasConstraintName("fk_tasks_reminders_settingReminder");

                entity.HasOne(d => d.IdfTaskNavigation)
                    .WithMany(p => p.tasks_reminders)
                    .HasForeignKey(d => d.IdfTask)
                    .HasConstraintName("fk_tasks_reminders_tasks");
            });

            modelBuilder.Entity<tasks_state_history>(entity =>
            {
                entity.HasIndex(e => e.IdfState)
                    .HasName("fk_tasks_sta_hist_Statuses_idx");

                entity.HasIndex(e => e.IdfTask)
                    .HasName("fk_tasks_sta_hist_tasks_idx");

                entity.HasIndex(e => e.IdfUser)
                    .HasName("fk_tasks_sta_hist_Identity_users_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.CurrentDate).HasColumnType("datetime");

                entity.Property(e => e.IdfState).HasColumnType("bigint(20)");

                entity.Property(e => e.IdfTask).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfUser).HasColumnType("bigint(10)");

                entity.HasOne(d => d.IdfStateNavigation)
                    .WithMany(p => p.tasks_state_history)
                    .HasForeignKey(d => d.IdfState)
                    .HasConstraintName("fk_tasks_sta_hist_Statuses");

                entity.HasOne(d => d.IdfTaskNavigation)
                    .WithMany(p => p.tasks_state_history)
                    .HasForeignKey(d => d.IdfTask)
                    .HasConstraintName("fk_tasks_sta_hist_tasks");

                entity.HasOne(d => d.IdfUserNavigation)
                    .WithMany(p => p.tasks_state_history)
                    .HasForeignKey(d => d.IdfUser)
                    .HasConstraintName("fk_tasks_sta_hist_Identity_users");
            });

            modelBuilder.Entity<time_tracking>(entity =>
            {
                entity.HasIndex(e => e.IdfStaffProjectPosition)
                    .HasName("fk_time_tracking_staffprojectposition_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfStaffProjectPosition).HasColumnType("bigint(10)");

                entity.Property(e => e.end).HasColumnType("datetime");

                entity.Property(e => e.endNote).HasColumnType("varchar(500)");

                entity.Property(e => e.start).HasColumnType("datetime");

                entity.Property(e => e.startNote).HasColumnType("varchar(1000)");

                entity.Property(e => e.status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdfStaffProjectPositionNavigation)
                    .WithMany(p => p.time_tracking)
                    .HasForeignKey(d => d.IdfStaffProjectPosition)
                    .HasConstraintName("fk_time_tracking_staffprojectposition");
            });

            modelBuilder.Entity<time_tracking_auto>(entity =>
            {
                entity.HasIndex(e => e.IdfUser)
                    .HasName("fk_time_tracking_auto_user_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfUser).HasColumnType("bigint(10)");

                entity.Property(e => e.start).HasColumnType("datetime");

                entity.HasOne(d => d.IdfUserNavigation)
                    .WithMany(p => p.time_tracking_auto)
                    .HasForeignKey(d => d.IdfUser)
                    .HasConstraintName("fk_time_tracking_auto_user");
            });

            modelBuilder.Entity<time_tracking_review>(entity =>
            {
                entity.HasIndex(e => e.IdfPeriod)
                    .HasName("fk_time_tracking_review_periods_idx");

                entity.HasIndex(e => e.IdfStaffProjectPosition)
                    .HasName("fk_time_tracking_review_staffprojectposition_idx");

                entity.Property(e => e.Id).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfPeriod).HasColumnType("bigint(10)");

                entity.Property(e => e.IdfStaffProjectPosition).HasColumnType("bigint(10)");

                entity.Property(e => e.SecondsModifiedTracking).HasColumnType("bigint(10)");

                entity.Property(e => e.SecondsScheduledTime).HasColumnType("bigint(10)");

                entity.Property(e => e.SecondsUserTracking).HasColumnType("bigint(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'C'");

                entity.HasOne(d => d.IdfPeriodNavigation)
                    .WithMany(p => p.time_tracking_review)
                    .HasForeignKey(d => d.IdfPeriod)
                    .HasConstraintName("fk_time_tracking_review_periods");

                entity.HasOne(d => d.IdfStaffProjectPositionNavigation)
                    .WithMany(p => p.time_tracking_review)
                    .HasForeignKey(d => d.IdfStaffProjectPosition)
                    .HasConstraintName("fk_time_tracking_review_staffprojectposition");
            });

            modelBuilder.Entity<todo_items>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Remind).HasColumnType("tinyint(4)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });
        }
    }
}
