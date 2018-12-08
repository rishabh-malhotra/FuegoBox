Alter Table Cart
Add UserID uniqueIdentifier not null;

Alter Table Cart
Add Constraint FK_CartUser
Foreign Key (UserID) References [User](ID)
