CREATE TABLE users (
	UserId int(11) NOT NULL AUTO_INCREMENT,
	Username varchar(32) NOT NULL,
	Password varchar(64) NOT NULL,
	
	JoinDate datetime NOT NULL DEFAULT current_timestamp(),
	
	Banned bool NOT NULL DEFAULT false,
	BannedReason text NOT NULL DEFAULT "",
	
	EmailAddress text NOT NULL DEFAULT "",
	
	
	
	Location text NOT NULL DEFAULT "",
	Occupation text NOT NULL DEFAULT "",
	Website text NOT NULL DEFAULT "",
	Twitter text NOT NULL DEFAULT "",
	Discord text NOT NULL DEFAULT "",
	
	
	
	StandardRankedScore bigint NOT NULL DEFAULT 0,
	TaikoRankedScore bigint NOT NULL DEFAULT 0,
	CatchRankedScore bigint NOT NULL DEFAULT 0,
	
	StandardTotalScore bigint NOT NULL DEFAULT 0,
	TaikoTotalScore bigint NOT NULL DEFAULT 0,
	CatchTotalScore bigint NOT NULL DEFAULT 0,
	
	
	
	StandardLevel double NOT NULL DEFAULT 0.0,
	TaikoLevel double NOT NULL DEFAULT 0.0,
	CatchLevel double NOT NULL DEFAULT 0.0,
	
	StandardPlaycount int NOT NULL DEFAULT 0,
	TaikoPlaycount int NOT NULL DEFAULT 0,
	CatchPlaycount int NOT NULL DEFAULT 0,
	
	StandardAccuarcy double NOT NULL DEFAULT 0.0,
	TaikoAccuarcy double NOT NULL DEFAULT 0.0,
	CatchAccuarcy double NOT NULL DEFAULT 0.0,

	
	
	StandardCountSSH int NOT NULL DEFAULT 0,
	TaikoCountSSH int NOT NULL DEFAULT 0,
	CatchCountSSH int NOT NULL DEFAULT 0,
	
	StandardCountSS int NOT NULL DEFAULT 0,
	TaikoCountSS int NOT NULL DEFAULT 0,
	CatchCountSS int NOT NULL DEFAULT 0,
	
	StandardCountSH int NOT NULL DEFAULT 0,
	TaikoCountSH int NOT NULL DEFAULT 0,
	CatchCountSH int NOT NULL DEFAULT 0,
	
	StandardCountS int NOT NULL DEFAULT 0,
	TaikoCountS int NOT NULL DEFAULT 0,
	CatchCountS int NOT NULL DEFAULT 0,
	
	StandardCountA int NOT NULL DEFAULT 0,
	TaikoCountA int NOT NULL DEFAULT 0,
	CatchCountA int NOT NULL DEFAULT 0,
	
	StandardCountB int NOT NULL DEFAULT 0,
	TaikoCountB int NOT NULL DEFAULT 0,
	CatchCountB int NOT NULL DEFAULT 0,
	
	StandardCountC int NOT NULL DEFAULT 0,
	TaikoCountC int NOT NULL DEFAULT 0,
	CatchCountC int NOT NULL DEFAULT 0,
	
	StandardCountD int NOT NULL DEFAULT 0,
	TaikoCountD int NOT NULL DEFAULT 0,
	CatchCountD int NOT NULL DEFAULT 0,
	
	
	
	StandardAcc300 int NOT NULL DEFAULT 0,
	TaikoAcc300 int NOT NULL DEFAULT 0,
	CatchAcc300 int NOT NULL DEFAULT 0,
	
	StandardAcc100 int NOT NULL DEFAULT 0,
	TaikoAcc100 int NOT NULL DEFAULT 0,
	CatchAcc100 int NOT NULL DEFAULT 0,
	
	StandardAcc50 int NOT NULL DEFAULT 0,
	TaikoAcc50 int NOT NULL DEFAULT 0,
	CatchAcc50 int NOT NULL DEFAULT 0,
	
	StandardAccGeki int NOT NULL DEFAULT 0,
	TaikoAccGeki int NOT NULL DEFAULT 0,
	CatchAccGeki int NOT NULL DEFAULT 0,
	
	StandardAccKatu int NOT NULL DEFAULT 0,
	TaikoAccKatu int NOT NULL DEFAULT 0,
	CatchAccKatu int NOT NULL DEFAULT 0,
	
	StandardAccMiss int NOT NULL DEFAULT 0,
	TaikoAccMiss int NOT NULL DEFAULT 0,
	CatchAccMiss int NOT NULL DEFAULT 0,
	
	
	
	ReplaysWatched int NOT NULL DEFAULT 0,
	Priviledges tinyint NOT NULL DEFAULT 0,
	
	PRIMARY KEY(UserId)
);