CREATE TABLE UserStatus (
	AccountStatus varChar(200) NOT NULL PRIMARY KEY
);
CREATE TABLE Users (
	UserEmail varChar(200) NOT NULL PRIMARY KEY,
	FirstName varChar(200),
	LastName varChar(200),
	DateOfBirth date,
	Gender varChar(200),
	AccountStatus varChar(200) FOREIGN KEY REFERENCES UserStatus(AccountStatus)

);

CREATE TABLE UserRole(
	RoleName varChar(200) NOT NULL PRIMARY KEY
);

CREATE TABLE Household(
	HouseholdID varChar(20) NOT NULL PRIMARY KEY,
	TotalMonthlyRent int,
	HouseholdAddress varChar(200),
);

CREATE TABLE HouseholdProfile(
	HouseholdID varChar(20) NOT NULL FOREIGN KEY REFERENCES Household(HouseholdID) PRIMARY KEY,
	NumberOfResidents int,
	HouseholdImages varChar(200),
	HouseholdType varChar(200),
	EstimatedRentCost int
);

CREATE TABLE NormalUser (
	UserEmail varChar(200) NOT NULL FOREIGN KEY REFERENCES Users(UserEmail) PRIMARY KEY,
	RoleName varChar(200) NOT NULL FOREIGN KEY REFERENCES UserRole(RoleName),
	HouseholdID varChar(20) FOREIGN KEY REFERENCES HouseHold(HouseHoldID)
);


CREATE TABLE UserProfile(
	UserEmail varChar(200) NOT NULL FOREIGN KEY REFERENCES NormalUser(UserEmail) PRIMARY KEY,
	/* TODO: Determine best way to store and access user profile pictures. currently favoring 
			 saving a reference to the image stored elsewhere in a directory */
	ProfileImageReference varChar(200),
	UserDescription varChar(2000)
);

CREATE TABLE Admins (
	UserEmail varChar(200) NOT NULL FOREIGN KEY REFERENCES Users(UserEmail) PRIMARY KEY,
	AdminID varChar(20)
);

