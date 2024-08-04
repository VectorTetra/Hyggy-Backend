DIRECTORIES DEFINITIONS OF BUSINESS LOGIC LAYER

#   DTO - contains transfer models (Data Transfer Object).
	These models transfer data between PRESENTATION LAYER and BUSINESS LOGIC LAYER (in both directions).
	In a multi-layer application architecture, the properties of these particular models need to be marked with validation attributes.
	You can use standard validation attributes by connecting using System.ComponentModel.DataAnnotations;
	If custom validation attributes are used, their classes are also contained in the BUSINESS LOGIC LAYER.

#   Interfaces - contains interfaces used by Services to interact with the database

#	Services - contains implementations of interfaces from the Interfaces folder and deals with business logic.
	Also, each of these services contains an injected IUnitOfWork reference.
	These services transfer data between DATA ACCESS LAYER and BUSINESS LOGIC LAYER (in both directions).
	It is in these services that domain models for DATA ACCESS LAYER are created based on transfer models.

	!	For example, in the ADD, UPDATE, DELETE methods transfer models from PRESENTATION LAYER are transferred,
	and on their basis domain models for DATA ACCESS LAYER are created.

	!	In GET methods you can try to get the domain model by Id,
		- in case of failure - return a ValidationException object to the controller method in PRESENTATION LAYER
		(ValidationException definition is in the Infrastructure folder)
		- if successful, a transfer model is created based on the domain model from the database, and then returned
		into a controller method in PRESENTATION LAYER

	! To simplify the creation of domain-based transfer models, you can use Mapping.
	To do this you need to install the AutoMapper package.

	! Example mapping for the same DTO and domain Model

		public async Task<IEnumerable<TeamDTO>> GetTeams()
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<Team>, IEnumerable<TeamDTO>>(await Database.Teams.GetAll());
		}

	! Example mapping for various DTOs and domain models

		public async Task<IEnumerable<PlayerDTO>> GetPlayers()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDTO>()
			.ForMember("Team", opt => opt.MapFrom(c => c.Team.Name)));
			var mapper = new Mapper(config);
			return mapper.Map<IEnumerable<Player>, IEnumerable<PlayerDTO>>(await Database.Players.GetAll());
		}

#	Infrastructure - contains extension methods for connecting database context services to the PRESENTATION LAYER,
	and the IUnitOfWork service (they are located in the DATA ACCESS LAYER).
	Also contains a custom class for handling validation errors ValidationException

		public class ValidationException : Exception
		{
			public string Property { get; protected set; }
			public ValidationException(string message, string prop) : base(message)
			{
				Property = prop;
			}
		}	

======================================================================================================================================

ОПРЕДЕЛЕНИЯ ПАПОК УРОВНЯ БИЗНЕС-ЛОГИКИ

#	DTO — содержит трансферные модели (Data Transfer Object). 
	Эти модели передают данные между PRESENTATION LAYER и BUSINESS LOGIC LAYER (в обе стороны).
	В многослойной архитектуре приложения свойства именно этих моделей нужно размечать атрибутами валидации.
	Использовать стандартные атрибуты валидации можно, подключив using System.ComponentModel.DataAnnotations;
	Если используются собственные атрибуты валидации, их классы также находятся в BUSINESS LOGIC LAYER.

#	Interfaces - содержит интерфейсы, используемые сервисами (Services) для взаимодействия с БД

#	Services — содержит реализации интерфейсов из папки Interfaces и занимаются бизнес-логикой.
	Также каждый из этих сервисов содержит инжектированную ссылку IUnitOfWork.
	Эти сервисы передают данные между DATA ACCESS LAYER и BUSINESS LOGIC LAYER (в обе стороны).
	Именно в этих сервисах на основе трансферных моделей создаются доменные модели для DATA ACCESS LAYER.
	!	Например, в методах ADD,UPDATE,DELETE передаются трансферные модели из PRESENTATION LAYER, 
		и на их основе создаются доменные модели для DATA ACCESS LAYER.
	!	В методах GET можно попытаться получить доменную модель по Id, 
		-	в случае неудачи - в метод контроллера в PRESENTATION LAYER вернуть обьект ValidationException 
			(определение ValidationException находится в папке Infrastructure)
		-	в случае успеха - создается трансферная модель на основе доменной модели из БД, и затем возвращается 
			в метод контроллера в PRESENTATION LAYER
	!	Для упрощения создания трансферных моделей на основе доменных, можно использовать Mapping.
		Для этого нужно установить пакет AutoMapper.
	!	Пример маппинга для одинаковых DTO и domain Model

		// Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<TeamDTO>> GetTeams()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Team>, IEnumerable<TeamDTO>>(await Database.Teams.GetAll());
        }
	!	Пример маппинга для различных DTO и domain Model

		public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDTO>()
            .ForMember("Team", opt => opt.MapFrom(c => c.Team.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Player>, IEnumerable<PlayerDTO>>(await Database.Players.GetAll());
        }


#	Infrastructure - содержит методы расширения для подключения в PRESENTATION LAYER сервисов контекста БД, 
	и сервиса IUnitOfWork (они находятся в DATA ACCESS LAYER). 
	Также содержит пользовательський класс для обработки ошибок валидации ValidationException

	public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }

======================================================================================================================================

ВИЗНАЧЕННЯ ПАПОК РІВНЯ БІЗНЕС-ЛОГІКИ

#	DTO - містить трансферні моделі (Data Transfer Object).
	Ці моделі передають дані між PRESENTATION LAYER та BUSINESS LOGIC LAYER (в обидві сторони).
	У багатошаровій архітектурі застосування якості саме цих моделей необхідно розмічати атрибутами валідації.
	Використовувати стандартні атрибути валідації можна, підключивши за допомогою System.ComponentModel.DataAnnotations;
	Якщо використовуються власні атрибути валідації, їх класи також перебувають у BUSINESS LOGIC LAYER.

#	Interfaces - містить інтерфейси, використовувані сервісами (Services) взаємодії з БД

#	Services - містить реалізації інтерфейсів з папки Interfaces та займаються бізнес-логікою.
	Також кожен із цих сервісів містить інжектоване посилання IUnitOfWork.
	Ці сервіси передають дані між DATA ACCESS LAYER та BUSINESS LOGIC LAYER (обидві сторони).
	Саме у цих сервісах на основі трансферних моделей створюються доменні моделі для DATA ACCESS LAYER.

	!	Наприклад, у методах ADD, UPDATE, DELETE передаються трансферні моделі з PRESENTATION LAYER,
		та на їх основі створюються доменні моделі для DATA ACCESS LAYER.
	!	У методах GET можна спробувати отримати доменну модель за Id,
		- у разі невдачі - у метод контролера в PRESENTATION LAYER повернути об'єкт ValidationException
		(Визначення ValidationException знаходиться в папці Infrastructure)
		- у разі успіху - створюється трансферна модель на основі доменної моделі з БД, а потім повертається
		метод контролера в PRESENTATION LAYER
	!	Для спрощення створення трансферних моделей на основі доменних можна використовувати Mapping.
		Для цього необхідно встановити пакет AutoMapper.
	!	Приклад мапінгу для однакових DTO та domain Model

// Automapper дозволяє проектувати одну модель на іншу, що дозволяє скоротити обсяги коду та спростити програму.
			public async Task<IEnumerable<TeamDTO>> GetTeams()
			{
				var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamDTO>()).CreateMapper();
				return mapper.Map<IEnumerable<Team>, IEnumerable<TeamDTO>>(await Database.Teams.GetAll());
			}
	! Приклад мапінгу для різних DTO та domain Model

			public async Task<IEnumerable<PlayerDTO>> GetPlayers()
			{
				var config = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDTO>()
				.ForMember("Team", opt => opt.MapFrom(c => c.Team.Name)));
				var mapper = New Mapper(config);
				return mapper.Map<IEnumerable<Player>, IEnumerable<PlayerDTO>>(await Database.Players.GetAll());
			}


#	Infrastructure - містить методи розширення для підключення до PRESENTATION LAYER сервісів контексту БД,
	та сервісу IUnitOfWork (вони знаходяться у DATA ACCESS LAYER).
	Також містить клас користувача для обробки помилок валідації ValidationException

			public class ValidationException : Exception
			{
				public string Property { get; protected set; }
				public ValidationException(string message, string prop) : base(message)
				{
					Property = prop;
				}
			}