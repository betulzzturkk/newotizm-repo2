CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [ParentName] nvarchar(100) NULL,
    [Specialization] nvarchar(200) NULL,
    [InstructorId] nvarchar(450) NULL,
    [RegistrationDate] datetime2 NOT NULL,
    [LastLoginDate] datetime2 NULL,
    [FirstName] nvarchar(100) NULL,
    [LastName] nvarchar(100) NULL,
    [Surname] nvarchar(100) NULL,
    [Role] nvarchar(50) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastLoginAt] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [ChildName] nvarchar(100) NULL,
    [ChildAge] int NULL,
    [ChildDiagnosis] nvarchar(200) NULL,
    [Address] nvarchar(500) NULL,
    [ProfilePicture] nvarchar(200) NULL,
    [Experience] int NULL,
    [EmergencyContact] nvarchar(100) NULL,
    [PreferredLanguage] nvarchar(50) NULL,
    [TimeZone] nvarchar(50) NULL,
    [NotificationsEnabled] bit NOT NULL,
    [ProfileImageUrl] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_AspNetUsers_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [AspNetUsers] ([Id])
);
GO


CREATE TABLE [FAQs] (
    [Id] int NOT NULL IDENTITY,
    [Question] nvarchar(500) NOT NULL,
    [Answer] nvarchar(2000) NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [OrderIndex] int NOT NULL,
    CONSTRAINT [PK_FAQs] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Information] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [PublishDate] datetime2 NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [Category] nvarchar(max) NOT NULL,
    [IsImportant] bit NOT NULL,
    [Tags] nvarchar(max) NULL,
    CONSTRAINT [PK_Information] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [MannerProgress] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [MannerName] nvarchar(max) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_MannerProgress] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AnimalProgress] (
    [Id] int NOT NULL IDENTITY,
    [AnimalId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [AnimalName] nvarchar(max) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    [LastUpdate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_AnimalProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnimalProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Announcements] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Content] nvarchar(1000) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [IsRead] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Announcements] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Announcements_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE SET NULL
);
GO


CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Children] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Age] int NOT NULL,
    [Diagnosis] nvarchar(500) NULL,
    [Interests] nvarchar(500) NULL,
    [SpecialNeeds] nvarchar(1000) NULL,
    [ParentId] nvarchar(450) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Children] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Children_AspNetUsers_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [ColorProgress] (
    [Id] int NOT NULL IDENTITY,
    [ColorName] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_ColorProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ColorProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [CompletionDate] datetime2 NULL,
    [InstructorId] nvarchar(450) NULL,
    [DifficultyLevel] int NOT NULL,
    [ImageUrl] nvarchar(200) NOT NULL,
    [DurationMinutes] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [IconClass] nvarchar(max) NOT NULL,
    [BackgroundColor] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [StudentCount] int NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_AspNetUsers_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [AspNetUsers] ([Id])
);
GO


CREATE TABLE [ExamResults] (
    [Id] int NOT NULL IDENTITY,
    [ExamLevel] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [CorrectCount] int NOT NULL,
    [WrongCount] int NOT NULL,
    [TotalQuestions] int NOT NULL,
    [Score] float NOT NULL,
    [CompletedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ExamResults] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExamResults_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Content] nvarchar(2000) NOT NULL,
    [Date] datetime2 NOT NULL,
    [SenderName] nvarchar(100) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [IsRead] bit NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [News] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Content] nvarchar(2000) NOT NULL,
    [Summary] nvarchar(500) NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [PublishDate] datetime2 NOT NULL,
    [AuthorId] nvarchar(450) NULL,
    [ImageUrl] nvarchar(200) NULL,
    CONSTRAINT [PK_News] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_News_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id])
);
GO


CREATE TABLE [NumberProgress] (
    [Id] int NOT NULL IDENTITY,
    [NumberValue] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_NumberProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NumberProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [ShapeProgress] (
    [Id] int NOT NULL IDENTITY,
    [ShapeName] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_ShapeProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShapeProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Surname] nvarchar(100) NOT NULL,
    [Age] int NOT NULL,
    [Gender] nvarchar(10) NOT NULL,
    [Diagnosis] nvarchar(500) NOT NULL,
    [Hobbies] nvarchar(500) NULL,
    [Notes] nvarchar(1000) NULL,
    [InstructorId] nvarchar(450) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_AspNetUsers_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [AspNetUsers] ([Id])
);
GO


CREATE TABLE [TaleProgress] (
    [Id] int NOT NULL IDENTITY,
    [TaleTitle] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Progress] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_TaleProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaleProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [TrafficSignProgress] (
    [Id] int NOT NULL IDENTITY,
    [SignName] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Progress] int NOT NULL,
    [InteractionCount] int NOT NULL,
    [LastInteraction] datetime2 NOT NULL,
    CONSTRAINT [PK_TrafficSignProgress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrafficSignProgress_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Activities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [Difficulty] nvarchar(50) NOT NULL,
    [IsCompleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Score] int NOT NULL,
    [DurationMinutes] int NOT NULL,
    [CompletionDate] datetime2 NULL,
    [UserId] nvarchar(450) NULL,
    [CourseId] int NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Activities_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Activities_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [ExamAnswers] (
    [Id] int NOT NULL IDENTITY,
    [ExamResultId] int NOT NULL,
    [QuestionIndex] int NOT NULL,
    [QuestionText] nvarchar(max) NOT NULL,
    [SelectedOption] nvarchar(max) NOT NULL,
    [CorrectOption] nvarchar(max) NOT NULL,
    [IsCorrect] bit NOT NULL,
    CONSTRAINT [PK_ExamAnswers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExamAnswers_ExamResults_ExamResultId] FOREIGN KEY ([ExamResultId]) REFERENCES [ExamResults] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Progresses] (
    [Id] int NOT NULL IDENTITY,
    [ChildId] int NOT NULL,
    [CourseId] int NOT NULL,
    [CompletionPercentage] int NOT NULL,
    [StudyTimeHours] real NOT NULL,
    [SuccessRate] int NOT NULL,
    [LastActivityDate] datetime2 NOT NULL,
    [Notes] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [StudentId] int NULL,
    CONSTRAINT [PK_Progresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Progresses_Children_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Children] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Progresses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]),
    CONSTRAINT [FK_Progresses_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
);
GO


CREATE TABLE [ActivityLogs] (
    [Id] int NOT NULL IDENTITY,
    [ActivityId] int NOT NULL,
    [ChildId] int NOT NULL,
    [Score] int NOT NULL,
    [DurationMinutes] int NOT NULL,
    [Status] nvarchar(50) NULL,
    [Notes] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CompletedAt] datetime2 NULL,
    CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ActivityLogs_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activities] ([Id]),
    CONSTRAINT [FK_ActivityLogs_Children_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Children] ([Id])
);
GO


CREATE INDEX [IX_Activities_CourseId] ON [Activities] ([CourseId]);
GO


CREATE INDEX [IX_Activities_UserId] ON [Activities] ([UserId]);
GO


CREATE INDEX [IX_ActivityLogs_ActivityId] ON [ActivityLogs] ([ActivityId]);
GO


CREATE INDEX [IX_ActivityLogs_ChildId] ON [ActivityLogs] ([ChildId]);
GO


CREATE UNIQUE INDEX [IX_AnimalProgress_UserId_AnimalId] ON [AnimalProgress] ([UserId], [AnimalId]);
GO


CREATE INDEX [IX_Announcements_UserId] ON [Announcements] ([UserId]);
GO


CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO


CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO


CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO


CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO


CREATE INDEX [IX_AspNetUsers_InstructorId] ON [AspNetUsers] ([InstructorId]);
GO


CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO


CREATE INDEX [IX_AspNetUserTokens_ApplicationUserId] ON [AspNetUserTokens] ([ApplicationUserId]);
GO


CREATE INDEX [IX_Children_ParentId] ON [Children] ([ParentId]);
GO


CREATE INDEX [IX_ColorProgress_UserId] ON [ColorProgress] ([UserId]);
GO


CREATE INDEX [IX_Courses_InstructorId] ON [Courses] ([InstructorId]);
GO


CREATE INDEX [IX_ExamAnswers_ExamResultId] ON [ExamAnswers] ([ExamResultId]);
GO


CREATE INDEX [IX_ExamResults_UserId] ON [ExamResults] ([UserId]);
GO


CREATE INDEX [IX_Messages_UserId] ON [Messages] ([UserId]);
GO


CREATE INDEX [IX_News_AuthorId] ON [News] ([AuthorId]);
GO


CREATE INDEX [IX_NumberProgress_UserId] ON [NumberProgress] ([UserId]);
GO


CREATE INDEX [IX_Progresses_ChildId] ON [Progresses] ([ChildId]);
GO


CREATE INDEX [IX_Progresses_CourseId] ON [Progresses] ([CourseId]);
GO


CREATE INDEX [IX_Progresses_StudentId] ON [Progresses] ([StudentId]);
GO


CREATE INDEX [IX_ShapeProgress_UserId] ON [ShapeProgress] ([UserId]);
GO


CREATE INDEX [IX_Students_InstructorId] ON [Students] ([InstructorId]);
GO


CREATE INDEX [IX_TaleProgress_UserId] ON [TaleProgress] ([UserId]);
GO


CREATE INDEX [IX_TrafficSignProgress_UserId] ON [TrafficSignProgress] ([UserId]);
GO


