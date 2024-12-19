graph TD
    User[User] {
        int id PK
        string firstName
        string lastName
        string email
        string password
        string phoneNumber
        boolean emailNotification
        datetime lastLogin
        datetime createdAt
        datetime updatedAt
        enum userRole
    }

    TaskItem[TaskItem] {
        int id PK
        string title
        string description
        enum status
        datetime date
        time startTime
        time endTime
        datetime updatedAt
    }

    TaskEvent[TaskEvent] {
        int id PK
        string title
        datetime dateForEvent
        int locationId FK
        string byCustomer
        string foodChoices
        string specialRequest
        int participants
    }

    Location[Location] {
        int id PK
        string name
        int eventId FK
    }

    Competency[Competency] {
        int id PK
        string name
    }

    UserCompetency[UserCompetency] {
        int userId FK
        int competencyId FK
    }

    UserEvent[UserEvent] {
        int userId FK
        int eventId FK
    }

    TaskAssignment[TaskAssignment] {
        int taskId FK
        int userId FK
    }

    User --- UserCompetency
    Competency --- UserCompetency
    User --- UserEvent
    TaskEvent --- UserEvent
    TaskEvent --- Location
    TaskItem --- TaskEvent
    TaskItem --- TaskAssignment
    User --- TaskAssignment
