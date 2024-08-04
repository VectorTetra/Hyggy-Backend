DIRECTORIES DEFINITIONS OF DATA ACCESS LAYER

#   EF - contains the database context class.

#   Entities - contains domain models. These models are used in the database.

#   Interfaces - contains the definition of a repository interface for working with domain models,
	and also contains the IUnitOfWork.cs interface for combining all repository interfaces. 
	!	Using a class with a UnitOfWork implementation allows you to reduce work with different repositories and ensures that
		that all repositories will use the same database context.
		To connect the database context in ASP.NET services, a method inside the business layer will be used.

#	Repositories - contains repository classes, which are implementations of repository interfaces from the Interfaces folder.
	Also contains an implementation of the IUnitOfWork.cs interface

======================================================================================================================================

ОПРЕДЕЛЕНИЯ ПАПОК УРОВНЯ ДОСТУПА ДАННЫХ
#	EF — содержит класс контекста базы данных.

#	Entities — содержит модели предметной области (доменные модели). Эти модели используются в базе данных.

#	Interfaces - содержит определение интерфейса репозитория для работы с доменными моделями,
	также содержит интерфейс IUnitOfWork.cs.
	!	Использование класса с реализацией UnitOfWork позволяет упростить работу с разными репозиториями и гарантирует, 
		что для всех репозиториев будет использоваться один контекст БД.
		Для подключения контекста БД в сервисы ASP.NET будет использоваться метод внутри Business Layer

#	Repositories - содержит классы репозиториев, которые являются реализациями интерфейсов репозиториев из папки Interfaces.
	Также содержит реализацию интерфейса IUnitOfWork.cs

======================================================================================================================================

КАТАЛОГИ ВИЗНАЧЕННЯ РІВНЯ ДОСТУПУ ДО ДАНИХ

# EF - містить клас контексту бази даних.

# Entities - містить моделі домену. Ці моделі використовуються в базі даних.

# Інтерфейси - містить визначення інтерфейсу репозиторію для роботи з моделями домену,
а також містить інтерфейс IUnitOfWork.cs для об’єднання всіх інтерфейсів сховища.
	!	Використання класу з реалізацією UnitOfWork дозволяє скоротити роботу з різними репозиторіями та гарантує, що
		що всі репозиторії використовуватимуть той самий контекст бази даних.
		Для підключення контексту бази даних у службах ASP.NET буде використано метод усередині бізнес-рівня.

# Repositories - містить класи репозиторію, які є реалізаціями інтерфейсів репозиторію з папки Interfaces.
Також містить реалізацію інтерфейсу IUnitOfWork.cs