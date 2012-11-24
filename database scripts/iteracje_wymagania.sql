USE tracktracer
GO

/* mo¿e byæ inna nazwa klucza obcego, u mnie jest FK__Wymagania__Itera__1FCDBCEB*/
alter table Wymagania drop constraint FK__Wymagania__Itera__1FCDBCEB;

create table Iteracje_Wymagania (
 id int IDENTITY(1,1),
 IteracjaId int not null,
 WymaganieId int not null,
 primary key (id),
 foreign key (IteracjaId) references Iteracje (id),
 foreign key (WymaganieId) references Wymagania (id),
 UNIQUE( IteracjaId, WymaganieId)
);