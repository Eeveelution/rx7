CREATE TABLE users (
	UserId int(11) NOT NULL AUTO_INCREMENT,
	Username varchar(32) NOT NULL,
	
	JoinDate datetime NOT NULL DEFAULT current_timestamp(),
	
	Banned bool NOT NULL DEFAULT false,
	BannedReason text NOT NULL DEFAULT "",
	
	EmailAddress text NOT NULL DEFAULT "",
	
	

	
	Location text NOT NULL DEFAULT "",
	Occupation text NOT NULL DEFAULT "",
	Website text NOT NULL DEFAULT "",
	Twitter text NOT NULL DEFAULT "",
	Discord text NOT NULL DEFAULT "";
	
	
	
	
	
	
	StandardRankedScore bigint NOT NULL,
	TaikoRankedScore bigint NOT NULL,
	CatchRankedScore bigint NOT NULL,
	
	StandardTotalScore bigint NOT NULL,
	TaikoTotalScore bigint NOT NULL,
	CatchTotalScore bigint NOT NULL,
	
	
	
	
	
	StandardLevel double NOT NULL,
	TaikoLevel double NOT NULL,
	CatchLevel double NOT NULL,
	
	StandardPlaycount int NOT NULL,
	TaikoPlaycount int NOT NULL,
	CatchPlaycount int NOT NULL,
	
	
	
	
	
	
	StandardCountSSH int NOT NULL,
	TaikoCountSSH int NOT NULL,
	CatchCountSSH int NOT NULL,
	
	StandardCountSS int NOT NULL,
	TaikoCountSS int NOT NULL,
	CatchCountSS int NOT NULL,
	
	StandardCountSH int NOT NULL,
	TaikoCountSH int NOT NULL,
	CatchCountSH int NOT NULL,
	
	StandardCountS int NOT NULL,
	TaikoCountS int NOT NULL,
	CatchCountS int NOT NULL,
	
	StandardCountA int NOT NULL,
	TaikoCountA int NOT NULL,
	CatchCountA int NOT NULL,
	
	StandardCountB int NOT NULL,
	TaikoCountB int NOT NULL,
	CatchCountB int NOT NULL,
	
	StandardCountC int NOT NULL,
	TaikoCountC int NOT NULL,
	CatchCountC int NOT NULL,
	
	StandardCountD int NOT NULL,
	TaikoCountD int NOT NULL,
	CatchCountD int NOT NULL,
	
	
	
	
	
	
	StandardAcc300 int NOT NULL,
	TaikoAcc300 int NOT NULL,
	CatchAcc300 int NOT NULL,
	
	StandardAcc100 int NOT NULL,
	TaikoAcc100 int NOT NULL,
	CatchAcc100 int NOT NULL,
	
	StandardAcc50 int NOT NULL,
	TaikoAcc50 int NOT NULL,
	CatchAcc50 int NOT NULL,
	
	StandardAccGeki int NOT NULL,
	TaikoAccGeki int NOT NULL,
	CatchAccGeki int NOT NULL,
	
	StandardAccKatu int NOT NULL,
	TaikoAccKatu int NOT NULL,
	CatchAccKatu int NOT NULL,
	
	StandardAccMiss int NOT NULL,
	TaikoAccMiss int NOT NULL,
	CatchAccMiss int NOT NULL,
	
	
	
	
	ReplaysWatched int NOT NULL,
	Priviledges tinyint NOT NULL,
);