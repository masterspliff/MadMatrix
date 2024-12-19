graph TD
    USER[User]
    TASK[Task]
    EVENT[Event]
    LOCATION[Location]
    COMPETENCY[Competency]

    %% User attributes
    USER -->|has| USER_ATTRS[["
        - name: string
        - email: string
        - phone: string
        - emailNotifications: boolean
        - lastLogin: datetime
    "]]

    %% Task attributes
    TASK -->|has| TASK_ATTRS[["
        - title: string
        - description: string
        - status: string
        - date: datetime
        - startTime: time
        - endTime: time
    "]]

    %% Event attributes
    EVENT -->|has| EVENT_ATTRS[["
        - title: string
        - eventDate: date
        - customer: string
        - foodChoices: string
        - specialRequest: string
        - participants: int
    "]]

    %% Location attributes
    LOCATION -->|has| LOCATION_ATTRS[["
        - name: string
    "]]

    %% Competency attributes
    COMPETENCY -->|has| COMPETENCY_ATTRS[["
        - name: string
    "]]

    %% Relationships
    USER --- COMPETENCY
    USER --- EVENT
    EVENT --- LOCATION
    EVENT --- TASK
    TASK --- USER
