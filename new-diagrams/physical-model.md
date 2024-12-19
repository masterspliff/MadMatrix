graph TD
    Users[Users] {
        int Id PK
        string FirstName "NOT NULL"
        string LastName "NOT NULL"
        string Email "NOT NULL, UNIQUE"
        string Password "NOT NULL"
        string PhoneNumber
        boolean EmailNotification "DEFAULT TRUE"
        datetime LastLogin "DEFAULT UTC_NOW"
        datetime CreatedAt "DEFAULT UTC_NOW"
        datetime UpdatedAt "DEFAULT UTC_NOW"
        enum UserRole "NOT NULL"
    }

    TaskItems[TaskItems] {
        int Id PK
        string Title "NOT NULL"
        string Description
        enum Status "DEFAULT 'NotStarted'"
        datetime Date "DEFAULT TODAY"
        time StartTime "NOT NULL"
        time EndTime "NOT NULL"
        datetime UpdatedAt "DEFAULT NOW"
    }

    TaskEvents[TaskEvents] {
        int Id PK
        string Title "NOT NULL"
        datetime DateForEvent "DEFAULT TODAY"
        int LocationId FK "NOT NULL"
        string ByCustomer "NOT NULL"
        string FoodChoices
        string SpecialRequest
        int Participants "DEFAULT 0"
    }

    Locations[Locations] {
        int Id PK
        string Name "NOT NULL"
        int EventId FK "NULL"
    }

    Competencies[Competencies] {
        int Id PK
        string Name "NOT NULL"
    }

    UserCompetencies[UserCompetencies] {
        int UserId FK "NOT NULL"
        int CompetencyId FK "NOT NULL"
        PRIMARY_KEY "(UserId, CompetencyId)"
    }

    UserEvents[UserEvents] {
        int UserId FK "NOT NULL"
        int EventId FK "NOT NULL"
        PRIMARY_KEY "(UserId, EventId)"
    }

    TaskAssignments[TaskAssignments] {
        int TaskId FK "NOT NULL"
        int UserId FK "NOT NULL"
        PRIMARY_KEY "(TaskId, UserId)"
    }

    Users --- UserCompetencies
    Competencies --- UserCompetencies
    Users --- UserEvents
    TaskEvents --- UserEvents
    TaskEvents --- Locations
    TaskItems --- TaskEvents
    TaskItems --- TaskAssignments
    Users --- TaskAssignments
