using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;

namespace JayGor.People.DataAccess
{
    public interface IDatabaseService
    {

        IdentityCustomentity GetMyAccount(string username);

        CommonResponse ChangeMyPassword(string username,
                                       string CurrentPassword,
                                       string NewPassword,
                                       string ConfirmNewPassword);

        string Ping();

        CommonResponse IdentityCreateUser(string email, string password);
        identity_users IdentityGetUserByCredentials(string email, string password);
        CommonResponse CommonSaveError(string errorDescription, long idfuser = 0);
        identity_users IdentityGetUserById(long id);
        identity_users IdentityGetUserByEmail(string mail);
        //IEnumerable<Identity_Roles> IdentityGetRoles();
        IEnumerable<Identity_RolesCustom> IdentityGetRoles(long IdStaff);

        // CLIENTS
        CommonResponse SaveClient(clients client, List<h_medical_remindersCustom> Reminders, List<ProjectClientCustomEntity> ProjectClient);
        //ClientCustomEntity GetClientById(long id);

        // ClientCustomEntity GetClientById(long id, out List<h_medical_remindersCustom> medicalReminders);
        ClientCustomEntity GetClientById(long id);

        IEnumerable<ClientCustomEntity> GetClients();

        IEnumerable<ClientCustomEntity> GetClientsByProjects(long projectId, long idPeriod);
        IEnumerable<ClientCustomEntity> GetClientsByProjectsAllPeriods(long projectId);

        //CLIENT FORMS
        IEnumerable<ClientFormsCustomEntity> GetAllClientForms();
        ClientFormsCustomEntity GetClientFormbyId(long id);
        CommonResponse SaveClientForm(client_forms clientForm);
        CommonResponse DeleteClientForms(long IdClientForm);
        IEnumerable<ClientFormbyClientCustomEntity> GetAllClientFormsByClient(long idClient);
        IEnumerable<ClientFormbyClientCustomEntity> GetAllClientFormsByClientandClientForm(long idClient, long idClientForm);
        CommonResponse SaveClientFormWithReminders(client_forms clientForm, client_form_reminders[] ClientFormReminders, FormFieldsCustomEntity[] FormFields);

        //CLIENT FORM FIELDS
        IEnumerable<ClientFormFieldsCustomEntity> GetAllClientFormFields();
        ClientFormFieldsCustomEntity GetClientFormFieldbyId(long id);
        CommonResponse SaveClientFormField(client_form_fields clientFormField);
        CommonResponse DeleteClientFormFields(long IdClientFormField);

        //CLIENT FORM VALUES
        IEnumerable<ClientFormValuesCustomEntity> GetAllClientFormValues();
        ClientFormValuesCustomEntity GetClientFormValuebyId(long id);
        CommonResponse SaveClientFormValue(client_form_values ClientFormValue);
        CommonResponse DeleteClientFormValues(long clientFormValueId);
        IEnumerable<ClientFormValuesCustomEntity> GetAllClientFormValuesByClientFormAndClient(long idClientForm, long idClient);
        CommonResponse SaveClientFormValueWithDetail(client_form_values ClientFormValue, client_form_field_values[] ClientFormFieldValues);

        //CLIENT FORM IMAGE VALUES
        IEnumerable<ClientFormImageValuesCustomEntity> GetAllClientFormImageValues();
        ClientFormImageValuesCustomEntity GetClientFormImageValuebyId(long id);
        CommonResponse SaveClientFormImageValue(client_form_image_values ClientFormImageValue);
        CommonResponse DeleteClientFormImageValues(long clientFormImageValueId);
        ClientFormImageValuesCustomEntity GetClientFormImageValueByClientFormAndClient(long idClientForm, long idClient);
        //bool UpdateClientFormValueImage(long idClientForm, long idClient, string fileName);

        //CLIENT FORM FIELD VALUES
        IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValues();
        ClientFormFieldValuesCustomEntity GetClientFormFieldValuebyId(long id);
        CommonResponse SaveClientFormFieldValue(client_form_field_values ClientFormFieldValue);
        CommonResponse DeleteClientFormFieldValues(long clientFormFieldValueId);
        IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValuesByClientFormAndClient(long idClientForm, long idClient);
        IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValuesByClientFormValue(long idClientFormValue);
        CommonResponse RemoveClientFormField(long clientFormId, long FormFieldId);
        CommonResponse AddClientFormField(long idClientForm, string name, string description, string placeholder, string dataType, string constraints);

        //CLIENTFORMREMINDERS
        IEnumerable<ClientFormRemindersCustomEntity> GetAllClientFormReminders();
        ClientFormRemindersCustomEntity GetClientFormReminderbyId(long id);
        CommonResponse SaveClientFormReminder(client_form_reminders ClientFormReminder);
        CommonResponse DeleteClientFormReminders(long clientFormReminderId);
        IEnumerable<ClientFormRemindersCustomEntity> GetAllClientFormRemindersByClientForm(long idClientForm);

        //CLIENTFORMREMINDERUSERS
        IEnumerable<ClientFormReminderUsersCustomEntity> GetAllClientFormReminderUsers();
        ClientFormReminderUsersCustomEntity GetClientFormReminderUserbyId(long id);
        CommonResponse SaveClientFormReminderUser(client_form_reminder_users ClientFormReminderUser);
        CommonResponse DeleteClientFormReminderUsers(long clientFormReminderUserId);
        IEnumerable<ClientFormReminderUsersCustomEntity> GetAllClientFormReminderUsersByClientFormReminder(long idClientFormReminder);


        List<tasks_reminders> GetTaskReminderByCurrentTime();
        List<DailyLogCustomEntity> GetDailyLogList(long periodId, long clientId);

        // List<IncidentCustomEntity> GetIncidentsList(long periodId, long clientId);             
        List<IncidentCustomEntity> GetIncidentsListByPeriod(long periodId);
        List<h_incident_involved_people> GetIncidentInvolvedPeopleById(long incidentId);
        List<InjuriesCustom> GetInjuriesListByClient(long idClient, long periodId);

        h_incidents GetIncidentById(long incidentId);
        //List<h_injury_type> GetInjuryTypes();
        List<h_degree_of_injury> GetDegreeOfInjuries();
        List<h_region> GetRegionInjuries();
        List<h_ministeries> GetMinisteries();

        List<h_catalogCustom> GetIncidentCatalog();
        List<h_catalogCustom> GetInjuryCatalog(); 
        // List<h_values> GetCatalogValues(long idIncident);

        List<ClientCustomEntity> GetClientsByIncident(long idIncident);


        List<h_injuries> GetInjuriesByIdIncident(long idincident);
        h_injuries GetInjuryById(long idInjury);

        List<h_type_serious_occurrence> GetTypeSeriousOccurrence();


        List<h_catalogCustom> GetInjuriesCatalog();


                              
        List<h_injury_values> GetCatalogInjuryValues(long idInjury);
        List<h_incident_values> GetCatalogIncidentValues(long idInjury);


        bool SaveInjury(h_injuries Injury,
                              List<h_injury_values> Catalog,
                              List<List<PointBody>> Points,
                              out long idInjuryOut);


        List<h_umab_intervention> GetUmabIntervention();

        bool SaveIncident(h_incidents Incident,
                                 List<h_incident_values> Catalog,
                                 List<ClientCustomEntity> Clients,
                                 List<h_injuries> Injuries,
                                 List<h_incident_involved_people> InvolvedPeople,
                                 out long idincidentOut);
                                 
        // only mobile  
        bool SaveDailyLogs(h_dailylogs DailyLog, List<h_dailylog_involved_people> InvolvedPeopl, out long idDailyLogOut);

        // DAILYLOG WEB
        h_dailylogs GetDailyLogById(long idDailyLog);
        List<h_dailylog_involved_people> GetDailyLogInvolvedPeopleById(long dailylogId);
        IEnumerable<StaffCustomEntity> GetStaffForDailyLog(long idperiod);

        // TASKS
        bool SaveTask(tasks task, duplicate_tasks duplicateTask, Boolean editingSerie);

        // bool CloneTask(long task, long IdfProjectm, long IdfPeriod, Boolean move);
        bool MoveCopyTask(tasks task, bool move, long idfProject, long idfPeriod);

        tasks GetTask(long id);
        IEnumerable<TasksForPlanningCustomEntity> GetTasksByStaff(long id, long idperiod);
        IEnumerable<TasksForPlanningCustomEntity> GetUnasignedTasks(DateTime from, DateTime to);
        IEnumerable<TasksForPlanningCustomEntity> GetTasks(DateTime from, DateTime to);
        List<tasks_reminders> GetTaskRemindes(long idperiod);
        List<tasks_reminders> GetTaskReminderByUser(long idUser);

        IEnumerable<statuses> GetStatuses();

        List<GenericPair> GetTasksForDashboard1(out string periodDescription);

        // void GetDashboard2(long idperiod, out long max, out List<string> colors,)
        List<tasks> GetDashboard2(out string periodDescription);
        List<GenericTriValue> GetDashboard3(string username, out string periodDescription);

        // PROJECTS
        IEnumerable<ProjectCustomEntity> GetProjectsByStaff(long idStaff, long idperiod);


        IEnumerable<ProjectCustomEntity> GetProjectsByClient(long idclient, long idperiod);


        List<ProjectClientCustomEntity> GetProjectClientByIdPeriodIdClient(long idperiod, long idclient);

        IEnumerable<ProjectCustomEntity> GetProjects();

        List<project_pettycash_categories> GetPettyCashCategories();

        IEnumerable<ProjectCustomEntity> GetProjectsByUser(string username);
        ProjectCustomEntity GetProject(long id);
        IEnumerable<TasksForPlanningCustomEntity> GetTasksByProject(long idProject);
        IEnumerable<TasksForPlanningCustomEntity> GetOverdueTasks();
        IEnumerable<TasksForPlanningCustomEntity> GetTasksByProject2(long idProject, long idperiod);
        List<settings_reminder_time> GetSettingsReminderTime();
        bool ResetPassword(long id, string newPassword);
        IEnumerable<StaffCustomEntity> GetStaffsByProject(long idProject);
        IEnumerable<StaffCustomEntity> GetStaffsByProject2(long idProject, long idperiod);

        staff_period_settings GetStaffPeriodSettings(long idStaff, long idPeriod);
        bool SaveStaffPeriodSettings(long idStaff, long idPeriod, int hours);

        long SaveProject(ProjectCustomEntity project,
                         List<TasksForPlanningCustomEntity> tasks,
                         List<Staff_Project_PositionCustomEntity> staffs,
                         List<ProjectOwnersCustom> owners,
                         List<tasks_reminders> tasksReminders,
                         string username,
                         long idperiod,
                         bool isadmin,
                         List<ClientCustomEntity> clients);

        bool DeleteProject(ProjectCustomEntity project, long idperiod);


        IEnumerable<ProjectOwnersCustom> GetProjectOwners(long idProject, long idPeriod);
        IEnumerable<ProjectPettyCashCustom> GetPettyCash(long idProject, long idperiod);
        IEnumerable<ProjectPettyCashCustom> GetPettyCashByCategory(long idProject, long idperiod, long idcategory);
        CommonResponse SavePettyCash(List<ProjectPettyCashCustom> pettyCash, string username, long idperiod);

        //PROJECT FORMS
        IEnumerable<ProjectFormsCustomEntity> GetAllProjectForms();
        ProjectFormsCustomEntity GetProjectFormbyId(long id);
        CommonResponse SaveProjectForm(project_forms projectForm);
        CommonResponse DeleteProjectForms(long IdProjectForm);
        IEnumerable<ProjectFormbyProjectCustomEntity> GetAllProjectFormsByProject(long idProject);
        IEnumerable<ProjectFormbyProjectCustomEntity> GetAllProjectFormsByProjectandProjectForm(long idProject, long idProjectForm);
        CommonResponse SaveProjectFormWithReminders(project_forms projectForm, project_form_reminders[] ProjectFormReminders, FormFieldsCustomEntity[] FormFields);

        //PROJECT FORM FIELDS
        IEnumerable<ProjectFormFieldsCustomEntity> GetAllProjectFormFields();
        ProjectFormFieldsCustomEntity GetProjectFormFieldbyId(long id);
        CommonResponse SaveProjectFormField(project_form_fields projectFormField);
        CommonResponse DeleteProjectFormFields(long IdProjectFormField);

        //PROJECT FORM VALUES
        IEnumerable<ProjectFormValuesCustomEntity> GetAllProjectFormValues();
        ProjectFormValuesCustomEntity GetProjectFormValuebyId(long id);
        CommonResponse SaveProjectFormValue(project_form_values ProjectFormValue);
        CommonResponse DeleteProjectFormValues(long projectFormValueId);
        IEnumerable<ProjectFormValuesCustomEntity> GetAllProjectFormValuesByProjectFormAndProject(long idProjectForm, long idProject);
        CommonResponse SaveProjectFormValueWithDetail(project_form_values ProjectFormValue, project_form_field_values[] ProjectFormFieldValues);

        //PROJECT FORM IMAGE VALUES
        IEnumerable<ProjectFormImageValuesCustomEntity> GetAllProjectFormImageValues();
        ProjectFormImageValuesCustomEntity GetProjectFormImageValuebyId(long id);
        CommonResponse SaveProjectFormImageValue(project_form_image_values ProjectFormImageValue);
        CommonResponse DeleteProjectFormImageValues(long projectFormImageValueId);
        ProjectFormImageValuesCustomEntity GetProjectFormImageValueByProjectFormAndProject(long idProjectForm, long idProject);
        //bool UpdateProjectFormValueImage(long idProjectForm, long idProject, string fileName);

        //PROJECT FORM FIELD VALUES
        IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValues();
        ProjectFormFieldValuesCustomEntity GetProjectFormFieldValuebyId(long id);
        CommonResponse SaveProjectFormFieldValue(project_form_field_values ProjectFormFieldValue);
        CommonResponse DeleteProjectFormFieldValues(long projectFormFieldValueId);
        IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValuesByProjectFormAndProject(long idProjectForm, long idProject);
        IEnumerable<ProjectFormFieldValuesCustomEntity> GetAllProjectFormFieldValuesByProjectFormValue(long idProjectFormValue);
        CommonResponse RemoveProjectFormField(long projectFormId, long FormFieldId);
        CommonResponse AddProjectFormField(long idProjectForm, string name, string description, string placeholder, string dataType, string constraints);

        //PROJECTFORMREMINDERS
        IEnumerable<ProjectFormRemindersCustomEntity> GetAllProjectFormReminders();
        ProjectFormRemindersCustomEntity GetProjectFormReminderbyId(long id);
        CommonResponse SaveProjectFormReminder(project_form_reminders ProjectFormReminder);
        CommonResponse DeleteProjectFormReminders(long projectFormReminderId);
        IEnumerable<ProjectFormRemindersCustomEntity> GetAllProjectFormRemindersByProjectForm(long idProjectForm);

        //PROJECTFORMREMINDERUSERS
        IEnumerable<ProjectFormReminderUsersCustomEntity> GetAllProjectFormReminderUsers();
        ProjectFormReminderUsersCustomEntity GetProjectFormReminderUserbyId(long id);
        CommonResponse SaveProjectFormReminderUser(project_form_reminder_users ProjectFormReminderUser);
        CommonResponse DeleteProjectFormReminderUsers(long projectFormReminderUserId);
        IEnumerable<ProjectFormReminderUsersCustomEntity> GetAllProjectFormReminderUsersByProjectFormReminder(long idProjectFormReminder);


        // STAFF
        IEnumerable<StaffCustomEntity> GetStaffsForOwnerList(long idProject, long idperiod);
        CommonResponse EnableDisableAccount(long IdUser, string State);
        IEnumerable<StaffCustomEntity> GetAllStaff();
        CommonResponse SaveStaff(staff Staff, identity_users user, List<long> roles, string password, int workingHoursByPeriodStaff, long idfPeriod);
        IEnumerable<StaffCustomEntity> GetStaffs();
        StaffCustomEntity GetStaffbyId(long id);
        IEnumerable<ProjectPositionCustomEntity> GetProjectPosisionCustom(long idStaff);
        IEnumerable<StaffForPlanningCustomEntity> GetStaffForPlanning(string groupby);
        IEnumerable<StaffForGeoTrackingCustomEntity> GetStaffForGeoTracking(long idperiod);
        IEnumerable<StaffForPlanningCustomEntity> GetStaffForScheduling(long idperiod);
        IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnProjects(long idperiod, string username);
        IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnScheduling(long idperiod, string username);
        IEnumerable<GeoTimeTracking> GetGeoTrackingData(DateTime datex);
        IEnumerable<GeoTimeTrackingAuto> GetGeoTrackingAutoData(DateTime datex);
        duplicate_tasks GetDuplicate(long id);
        // IEnumerable<StaffForPlanningCustomEntity> GetStaffForIncident(long idperiod);
        IEnumerable<StaffCustomEntity> GetStaffForIncident(long idperiod);
        
        //STAFF FORMS
        IEnumerable<StaffFormsCustomEntity> GetAllStaffForms();
        StaffFormsCustomEntity GetStaffFormbyId(long id);
        CommonResponse SaveStaffForm(staff_forms staffForm);
        CommonResponse DeleteStaffForms(long IdStaffForm);
        IEnumerable<StaffFormbyStaffCustomEntity> GetAllStaffFormsByStaff(long idStaff);
        IEnumerable<StaffFormbyStaffCustomEntity> GetAllStaffFormsByStaffandStaffForm(long idStaff, long idStaffForm);
        CommonResponse SaveStaffFormWithReminders(staff_forms staffForm, staff_form_reminders[] StaffFormReminders, FormFieldsCustomEntity[] FormFields);

        //FORMFIELDS
        IEnumerable<FormFieldsCustomEntity> GetAllFormFields();
        FormFieldsCustomEntity GetFormFieldbyId(long id);
        CommonResponse SaveFormField(form_fields Form_fields);
        CommonResponse DeleteFormField(long formfieldsId);
        IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByClientForm(long IdClientForm);
        IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByProjectForm(long IdProjectForm);
        IEnumerable<FormFieldsCustomEntity> GetAllFormFieldsByStaffForm(long IdStaffForm);

        //STAFFFORMFIELDS
        IEnumerable<StaffFormFieldsCustomEntity> GetAllStaffFormFields();
        StaffFormFieldsCustomEntity GetStaffFormFieldbyId(long id);
        CommonResponse SaveStaffFormField(staff_form_fields staffFormField);
        CommonResponse DeleteStaffFormFields(long IdStaffFormField);

        //STAFFFORMVALUES
        IEnumerable<StaffFormValuesCustomEntity> GetAllStaffFormValues();
        StaffFormValuesCustomEntity GetStaffFormValuebyId(long id);
        CommonResponse SaveStaffFormValue(staff_form_values StaffFormValue);
        CommonResponse DeleteStaffFormValues(long staffFormValueId);
        IEnumerable<StaffFormValuesCustomEntity> GetAllStaffFormValuesByStaffFormAndStaff(long idStaffForm, long idStaff);
        CommonResponse SaveStaffFormValueWithDetail(staff_form_values StaffFormValue, staff_form_field_values[] StaffFormFieldValues);
        
        //STAFFFORMIMAGEVALUES
        IEnumerable<StaffFormImageValuesCustomEntity> GetAllStaffFormImageValues();
        StaffFormImageValuesCustomEntity GetStaffFormImageValuebyId(long id);
        CommonResponse SaveStaffFormImageValue(staff_form_image_values StaffFormImageValue);
        CommonResponse DeleteStaffFormImageValues(long staffFormImageValueId);
        StaffFormImageValuesCustomEntity GetStaffFormImageValueByStaffFormAndStaff(long idStaffForm, long idStaff);
        //bool UpdateStaffFormValueImage(long idStaffForm, long idStaff, string fileName);

        //STAFFFORMFIELDVALUES
        IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValues();
        StaffFormFieldValuesCustomEntity GetStaffFormFieldValuebyId(long id);
        CommonResponse SaveStaffFormFieldValue(staff_form_field_values StaffFormFieldValue);
        CommonResponse DeleteStaffFormFieldValues(long staffFormFieldValueId);
        IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValuesByStaffFormAndStaff(long idStaffForm, long idStaff);
        IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValuesByStaffFormValue(long idStaffFormValue);
        CommonResponse RemoveStaffFormField(long staffFormId, long FormFieldId);
        CommonResponse AddStaffFormField(long idStaffForm, string name, string description, string placeholder, string dataType, string constraints);

        //STAFFFORMREMINDERS
        IEnumerable<StaffFormRemindersCustomEntity> GetAllStaffFormReminders();
        StaffFormRemindersCustomEntity GetStaffFormReminderbyId(long id);
        CommonResponse SaveStaffFormReminder(staff_form_reminders StaffFormReminder);
        CommonResponse DeleteStaffFormReminders(long staffFormReminderId);
        IEnumerable<StaffFormRemindersCustomEntity> GetAllStaffFormRemindersByStaffForm(long idStaffForm);

        //STAFFFORMREMINDERUSERS
        IEnumerable<StaffFormReminderUsersCustomEntity> GetAllStaffFormReminderUsers();
        StaffFormReminderUsersCustomEntity GetStaffFormReminderUserbyId(long id);
        CommonResponse SaveStaffFormReminderUser(staff_form_reminder_users StaffFormReminderUser);
        CommonResponse DeleteStaffFormReminderUsers(long staffFormReminderUserId);
        IEnumerable<StaffFormReminderUsersCustomEntity> GetAllStaffFormReminderUsersByStaffFormReminder(long idStaffFormReminder);

        // PERIODS
        IEnumerable<PeriodsCustom> GetPeriods();
        PeriodsCustom GetPeriod(long id);
        CommonResponse SavePeriods(List<PeriodsCustom> periods, int workingHoursDefaultByPeriodStaff);

        // SCHEDULING
        IEnumerable<SchedulingCustomEntity> GetScheduling(long period);
        IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long id);
        IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long id, long period);
        IEnumerable<SchedulingCustomEntity> GetSchedulingByProject(long project, long period);
        IEnumerable<SchedulingCustomEntity> GetSchedulingByOwnProjects(long idperiod, string username);
        IEnumerable<SchedulingCustomEntity> GetSchedulingByOwnScheduling(long period, string username);
        CommonResponse SaveScheduling(long period, List<long> StaffsIds, DateTime Date, DateTime Time1, DateTime Time2, duplicate_scheduling DuplicateScheduling);
        CommonResponse UpdateScheduling(long id, DateTime Date, DateTime Time1, DateTime Time2);
        CommonResponse DeleteScheduling(long id);

        // REPORTS
        IEnumerable<tasks> GetReport1(List<long> projectIds, DateTime From, DateTime To);
        IEnumerable<ReportProjectsDetailsCustomEntity> GetReportProjects();

        // FINANCE
        IEnumerable<TimeTrackingReviewCustom> GetTimeTrackingReviewResponse(long idPeriod);
        CommonResponse SaveTimeTrackingReview(List<TimeTrackingReviewCustom> tracking);
        bool UpdateIdentityImage(long id, string fileName);
        bool UpdateClientImage(long id, string fileName);
        // POSITIONS
        IEnumerable<positions> GetPositions();
        CommonResponse SavePositions(List<PositionsCustom> Positions);
        IEnumerable<positions> GetPositionsForCopyWindow(long idProject, long idperiod);

        IEnumerable<StaffCustomEntity> GetStaffForCopyWindow(long idProject, long idperiod);
        ClientCustomEntity GetClientByEmail(string email);
        List<TaskHistoryReportCustomEntity> GetTaskHistoryReport(long idProject, long idPeriod);

        // MOBILE
        List<ScheduleMobile> GetScheduleByUser(long idUser, DateTime date1, DateTime date2);
        List<StaffProjectPositionCustomEntity> GetProjectsPositionsbyUser(long idUser, long idCurrentPeriod);
        List<MyTasks_Mobile> GetMyTasks_Mobile(long idUser, long idCurrentPeriod);
        List<ProjectsDailyLogs_Mobile> GetProjectsDailyLogs_Mobile(long idUser);
        List<ClientsDailyLogs_Mobile> GetClientsDailyLogs_Mobile(long idUser);
        List<TimeTracker_Mobile> GetTimeTracker_Mobile(long idUser, DateTime date1);
        List<DailyLogs_Mobile> GetDailyLogsByUserId_Mobile(long idUser);
        bool SaveAutoGeoTracking(long idfUser, float latitude, float longitude, DateTime date1);
        bool ChangeTaskState(long IdfState, long IdTask, long IdUser, DateTime date1);
        bool StartTimeTracker(long IdfStaffProjectPosition, string startNote, float Longitude, float Latitude,DateTime date1);
        bool StopTimeTracker(long Id, string endNote, float endLong, float endLat, DateTime date1);
        bool SaveDailyLog(long Id,
                            long ProjectId,
                            long ClientId,
                            long UserId,
                            string Placement,
                            string StaffOnShift,
                            string GeneralMood,
                            string InteractionStaff,
                            string InteractionPeers,
                            string School,
                            string Attended,
                            string InHouseProg,
                            string Comments,
                            string Health,
                            string ContactFamily,
                            string SeriousOccurrence,
                            string Other,
							string State,
							DateTime date1);

        bool SaveFace(string email, string face);

        // GetDailyLogsByClientProject(projectId, clientId)
                       
        // = = = = = = = =   CHAT   = = = = = = = = = = = = 

        List<IdentityusersCustom_Chat> GetUsersForPush_Chat();

        List<RoomChat> GetRooms_Chat(long idUser);
        List<ParticipantRoomChat> GetRoomParticipant_Chat(long idChatRoom); 
        List<MessageChat> GetUnDeliveredMessages_Chat(long idUser);
        List<MessageChat> GetMessages_Chat(long idUser, long idChatRoom, long lastMessageId = 0);

        void UpdateIdentityUserTimeStamp_Chat(long idUser, long roomVersion, long participantsVersion, long messagesVersion,string updateClientORserver);
        chat_identity_users_timestamp GetIdentityUserTimeStamp_Chat(long idUser);
        List<ParticipantUserChat> GetGlobalParticipant_Chat();

        bool CreateRoom_Chat(string roomName, List<ParticipantUserChat> participants);
        bool RemoveParticipants_Chat(long idRoomChat, List<ParticipantRoomChat> participants);
        bool SendMessage(long idUser, long idRoom, string msg);
        bool UpdateMessageState_Chat(long UserId, List<long> DeliveredMessagesIds, List<long> ReadMessagesIds);
        bool CheckIfStaffProjectPositionInLastPeriod(long idspp);
        long GetLastActivePeriod();
        GenericPair GetLastActivePeriodAndDesc();

        List<IdentityusersCustom_Chat> GetUsersForPush_Chat2();
        bool IdentityUpdateIdOneSignal(long id, string idOneSignal);
        bool IdentityUpdateIdOneSignaWeb(long id, string idOneSignal);
        bool IdentityUpdateTFASecret(long id, string TFASecret);

        List<StaffCustomEntity> GetStaffsByClient(long idClient, long idperiod=0);
        List<h_medical_remindersCustom> GetMedicalRemindersByClient(long idClient);
        List<h_medical_remindersCustom> GetMedicalRemindersByUser(long idUser);
        List<h_medical_remindersCustom> GetMedicalRemindersByCurrentTime();

        List<h_medical_remindersCustom> GetMedicalRemindersByPeriod(long period);
        List<h_medical_remindersCustom> GetMedicalRemindersByPeriodAndOwnerProjects(long period);
        List<h_medical_remindersCustom> GetMedicalRemindersByPeriodAndAssignedToMe(long period, string username);

        bool DeleteSelectedSchedules(List<long> listaIdSchedules);
    }
}