using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RapidDoc.Models.Repository
{
    public enum AllWSType : byte
    {
        [Display(Name = "Носимая")]
        Wearable = 0,

        [Display(Name = "Автомобильная")]
        Car = 1,

        [Display(Name = "Стационарная")]
        Stationary = 2
    }

    public enum WSType : byte
    {
        [Display(Name = "Автомобильная")]
        Car = 0,

        [Display(Name = "Стационарная")]
        Stationary = 1
    }

    public enum WSComponents : byte
    {
        [Display(Name = "Антенна автомобильная")]
        CarAntenna = 0,

        [Display(Name = "Антенна стационарная")]
        StationaryAntenna = 1,

        [Display(Name = "Преобразователь автомобильный")]
        CarInverter = 2,

        [Display(Name = "Преобразователь стационарный")]
        StationaryInverter = 3,

        [Display(Name = "Кабель RG-58")]
        Cable_RG58 = 4
    }

    public enum PhoneType : byte
    {
        [Display(Name = "Аналоговый")]
        Analog = 0,

        [Display(Name = "IP телефон")]
        IP = 1,

        [Display(Name = "Цифровой")]
        Digital = 2
    }

    public enum WorkPlaceMovement : byte
    {
        [Display(Name = "Перемещение")]
        Movement = 0,

        [Display(Name = "Освобождение")]
        Release = 1
    }

    public enum ActionsPhone : byte
    {
        [Display(Name = "Удаление")]
        Delete = 0,

        [Display(Name = "Резервирование")]
        Reservation = 1
    }

    public enum CTS_ServiceList : byte
    {
        [Display(Name = "Телефонная связь и АТС")]
        TelephonePBX = 0,

        [Display(Name = "Тарификатор PBX Avaya")]
        TarificatorPBXAvaya = 1,

        [Display(Name = "Радиосвязь")]
        RadioCommunication = 2,

        [Display(Name = "Радиорелейная связь")]
        MicrowaveCommunication = 3,

        [Display(Name = "Аудио конференцсвязь")]
        AudioConferencing = 4,

        [Display(Name = "IP телефония")]
        IPTelephony = 5,

        [Display(Name = "СКС")]
        SCS = 6,

        [Display(Name = "Другое")]
        Other = 7
    }

    public enum ForwardType : byte
    {
        [Display(Name = "Нет")]
        None = 0,

        [Display(Name = "Безусловная")]
        Unconditional = 1,

        [Display(Name = "По занятости")]
        Employment = 2,

        [Display(Name = "Нет ответа")]
        NoAnswer = 3
    }

    public enum ConferenceType : byte
    {
        [Display(Name = "Нет")]
        None = 0,

        [Display(Name = "Внутренняя")]
        Internal = 1,

        [Display(Name = "Внешняя")]
        External = 2,
    }

    public enum ServiceType : byte
    {
        [Display(Name = "Нет")]
        None = 0,

        [Display(Name = "Call-appr логическая линия")]
        CallAppr = 1,

        [Display(Name = "Autodial автоматический набор")]
        Autodial = 2,

        [Display(Name = "Busy-ind автоматический набор с функцией занятости абонента")]
        BusyInd = 3,

        [Display(Name = "Call-fwd самостоятельная настройка переадресации")]
        CallFwd = 4,

        [Display(Name = "Abr-prog самостоятельное программирование автоматического набора")]
        AbrProg = 5,

        [Display(Name = "Dn-dst функция не беспокоить")]
        DnDst = 6
    }

    public enum ProblemTypeCTS : byte
    {
        [Display(Name = "Другое")]
        Other = 0,

        [Display(Name = "Низкое качество звука")]
        PoorSoundQuality = 1,

        [Display(Name = "Односторонняя слышимость")]
        UnilateralAudibility = 2,

        [Display(Name = "Тишина в трубке")]
        SilenceTube = 3,

        [Display(Name = "Не проходят звонки")]
        CallsNotPass = 4,

        [Display(Name = "Нет исходящих вызовов")]
        NoOutgoingCalls = 5,

        [Display(Name = "Замена провода")]
        ReplacementCable = 6,
    }

    public enum AccessRight1C77 : byte
    {
        [Display(Name = "Просмотр")]
        View = 0,

        [Display(Name = "Изменение")]
        Edit = 1
    }

    public enum StorageType : byte
    {
        [Display(Name = "USB flash disk")]
        USBflashDisk = 0,

        [Display(Name = "DVD disk")]
        DVDdisk = 1,

        [Display(Name = "CD disk")]
        CDdisk = 2,

        [Display(Name = "USB HARD disk")]
        USBHARDdisk = 3
    }

    public enum StorageVolume : byte
    {
        [Display(Name = "4Gb")]
        V4Gb = 0,

        [Display(Name = "8Gb")]
        V8Gb = 1,

        [Display(Name = "160Gb")]
        V160Gb = 2,

        [Display(Name = "700Mb")]
        V700Mb = 3,

        [Display(Name = "250Gb")]
        V250Gb = 4,

        [Display(Name = "320Gb")]
        V320Gb = 5,

        [Display(Name = "500Gb")]
        V500Gb = 6,

        [Display(Name = "640Gb")]
        V640Gb = 7,

        [Display(Name = "750Gb")]
        V750Gb = 8,

        [Display(Name = "1Tb")]
        V1Tb = 9,

        [Display(Name = "1,5Tb")]
        V15Tb = 10,

        [Display(Name = "2Tb")]
        V2Tb = 11
    }

    public enum DeleteSignLotus : byte
    {
        [Display(Name = "Подпись")]
        Document = 0,

        [Display(Name = "Документ")]
        Signature = 1
    }

    public enum AccessRightBasic : byte
    {
        [Display(Name = "Просмотр")]
        View = 0,

        [Display(Name = "Изменение")]
        Edit = 1
    }

    public enum BlocksATK : byte
    {
        [Display(Name = "АДМИНИСТРАТИВНЫЙ БЛОК")]
        AdministrativeUnit = 0,

        [Display(Name = "БЛОК ПО ИНВЕСТИЦИЯМ и КАПИТАЛЬНОМУ СТРОИТЕЛЬСТВУ")]
        InvestBlock = 1,

        [Display(Name = "КОММЕРЧЕСКИЙ БЛОК")]
        ComerBlock = 2,

        [Display(Name = "ФИНАНСОВЫЙ БЛОК")]
        FinanceBlock = 3,

        [Display(Name = "БЛОК ПРОМЫШЛЕННОЙ БЕЗОПАСНОСТИ и ВСПОМОГАТЕЛЬНОГО ПРОИЗВОДСТВА")]
        AuxiliaryBlock = 4,

        [Display(Name = "БЛОК ГОРНОГО ПРОИЗВОДСТВА")]
        MiningBlock = 5,

        [Display(Name = "БЛОК ОБОГАЩЕНИЯ")]
        ConcetrationBlock = 6,

        [Display(Name = "ТОО Altyntau Kokshetau")]
        ATK = 8
    }

    public enum HardSoftwareMaintenance : byte
    {
        [Display(Name = "Сетевое оборудование")]
        NetworkEquipment = 0,

        [Display(Name = "Сервер")]
        Server = 1,

        [Display(Name = "Microsoft Exchange")]
        MicrosoftExchange = 2,

        [Display(Name = "Lotus Notes")]
        LotusNotes = 3,

        [Display(Name = "Microsoft DAX")]
        MicrosoftDAX = 4,

        [Display(Name = "1C")]
        ERP1C = 5,

        [Display(Name = "Microsoft SQL")]
        MicrosoftSQL = 6,

        [Display(Name = "Прочее программное обеспечение")]
        OtherSoftware = 7,

        [Display(Name = "Прочее оборудование")]
        Other = 8,

        [Display(Name = "Интернет")]
        Internet = 9,

        [Display(Name = "Active Directory")]
        AD = 10,

        [Display(Name = "VPN")]
        VPN = 11,

        [Display(Name = "Терминал-сервер")]
        Terminal = 12,

        [Display(Name = "Файловые ресурсы")]
        ShareFile = 13,

        [Display(Name = "СУД")]
        SUD = 14
    }

    public enum ServiceIncidientPriority : byte
    {
        [Display(Name = "Средний")]
        Medium = 0,

        [Display(Name = "Критический")]
        Critical = 1,

        [Display(Name = "Высокий")]
        High = 2,

        [Display(Name = "Низкий")]
        Low = 3
    }

    public enum ServiceIncidientLevel : byte
    {
        [Display(Name = "1 линия поддержки")]
        OneLevel = 0,

        [Display(Name = "2 линия поддержки")]
        TwoLevel = 1
    }

    public enum ServiceIncidientLocation : byte
    {
        [Display(Name = "ATK")]
        Element1 = 0,

        [Display(Name = "ЗИФ")]
        Element2 = 1
    }

    public enum AddOrChange : byte
    {
        [Display(Name = "Разработать")]
        Element1 = 0,

        [Display(Name = "Модифицировать")]
        Element2 = 1
    }

    public enum TranslateDirection : byte
    {
        [Display(Name = "С русского на английский")]
        Element1 = 0,

        [Display(Name = "С английского на русский")]
        Element2 = 1
    }

    public enum TranslateDirectionKAZ : byte
    {
        [Display(Name = "С русского на казахский")]
        Element1 = 0,

        [Display(Name = "С казахского на русский")]
        Element2 = 1
    }

    public enum TypeJUDocument : byte
    {
        [Display(Name = "Копия")]
        Element1 = 0,

        [Display(Name = "Оригинал")]
        Element2 = 1
    }

    public enum PipeName : byte
    {
        [Display(Name = "Хоз. Пит.")]
        Element1 = 0,

        [Display(Name = "Чаглинский пром. -хоз.водозабор")]
        Element2 = 1,

        [Display(Name = "Мырзакольсорский пром.водозабор")]
        Element3 = 2
    }

    public enum ContragentType : byte
    {
        [Display(Name = "Клиент")]
        Сustomer = 0,

        [Display(Name = "Поставщик")]
        Provider = 1,

        [Display(Name = "Клиент\\Поставщик")]
        Both = 2
    }

    public enum ObjectAccess : byte
    {
        [Display(Name = "АБК")]
        Element1 = 0,

        [Display(Name = "ЗИФ/РЗ")]
        Element2 = 1,

        [Display(Name = "ЗИФ/ОРЗ")]
        Element3 = 2,

        [Display(Name = "Пром. Площадка")]
        Element4 = 2
    }

    public enum Warehouse : byte
    {
        [Display(Name = "ГП ТМЦ (УГР, УТОР)")]
        Element1 = 0,

        [Display(Name = "ГСМ Управление техобслуживания и ремонта")]
        Element2 = 1,

        [Display(Name = "ГСМ_Участок буровзрывных работ")]
        Element3 = 2,

        [Display(Name = "ГСМ_Участок содержания дорог и отвалов")]
        Element4 = 3,

        [Display(Name = "ГСМ_Участок экскавации и транспортировки")]
        Element5 = 4,

        [Display(Name = "Забалансовый склад УТ Цех вспомогательного транспорта")]
        Element6 = 5,

        [Display(Name = "Забалансовый склад УТ Цех легкового транспорта")]
        Element7 = 6,

        [Display(Name = "Забалансовый склад УТЦОиР")]
        Element8 = 7,

        [Display(Name = "Забалансовый склад УТ Цех пассажирского транспорта")]
        Element9 = 8,

        [Display(Name = "ЗИФ Служба АСУ ТП")]
        Element10 = 9,

        [Display(Name = "ЗИФ отделение гидрометалургии")]
        Element11 = 10,

        [Display(Name = "ЗИФ Дробильное отделение")]
        Element12 = 11,

        [Display(Name = "ЗИФ ДО (Среднее дробление)")]
        Element13 = 12,

        [Display(Name = "ЗИФ Отделение сгущения, произв. водоснабжения и хвостового хозяйства")]
        Element14 = 13,

        [Display(Name = "ЗИФ Отделение флотации и гравитации")]
        Element15 = 14,

        [Display(Name = "ЗИФ Реагентное отделение")]
        Element16 = 15,

        [Display(Name = "ЗИФ Служба главного энергетика")]
        Element17 = 16,

        [Display(Name = "ЗИФ Хозяйственная служба")]
        Element18 = 17,

        [Display(Name = "Склад неликвидов")]
        Element19 = 18,

        [Display(Name = "Склад отдела охраны здоровья и техники безопасности")]
        Element20 = 19,

        [Display(Name = "Организационно-контрольная служба")]
        Element21 = 20,

        [Display(Name = "ЗИФ Отделение тонкого дробления и измельчения Корпус измельчения")]
        Element22 = 21,

        [Display(Name = "ЗИФ Отделение тонкого дробления и измельчения Корпус тонкого дробления")]
        Element23 = 22,

        [Display(Name = "Пробирно-аналитическая лаборатория (золото)")]
        Element24 = 23,

        [Display(Name = "Реагентное отделение СУиХ")]
        Element25 = 24,

        [Display(Name = "Отдел технического контроля")]
        Element26 = 25,

        [Display(Name = "Пробирно-аналитическая лаборатория")]
        Element27 = 26,

        [Display(Name = "Центральный склад СДЯВ")]
        Element28 = 27,

        [Display(Name = "Служба по связям с общественностью")]
        Element29 = 28,

        [Display(Name = "Служба учета и хранения")]
        Element30 = 29,

        [Display(Name = "Управление безопасности")]
        Element31 = 30,

        [Display(Name = "Управление информационных технологий")]
        Element32 = 31,

        [Display(Name = "Участок кучного выщелачивания")]
        Element33 = 32,

        [Display(Name = "Управление по работе с персоналом")]
        Element34 = 33,

        [Display(Name = "Управление по развитию персонала СОР")]
        Element35 = 34,

        [Display(Name = "Управление транспорта")]
        Element36 = 35,

        [Display(Name = "УТОР ЦРМ")]
        Element37 = 36,

        [Display(Name = "Служба Охраны ОС")]
        Element38 = 37,

        [Display(Name = "Сектор переводов ОКС")]
        Element39 = 38,

        [Display(Name = "Управление безопасности_золото")]
        Element40 = 39,

        [Display(Name = "УКВ золото")]
        Element41 = 40,

        [Display(Name = "ГСМ Управления транспорта")]
        Element42 = 41,

        [Display(Name = "Цех железнодорожных перевозок")]
        Element43 = 42,

        [Display(Name = "Управление технического планирования горных работ")]
        Element44 = 43,

        [Display(Name = "Объединенная котельная")]
        Element45 = 44,

        [Display(Name = "Цех сетей и подстанций")]
        Element46 = 45,

        [Display(Name = "Цех тепло и водоснабжения")]
        Element47 = 46,

        [Display(Name = "ХУ Коттедж")]
        Element48 = 47,

        [Display(Name = "ХУ Отдел эксплуатации здания")]
        Element49 = 48,

        [Display(Name = "ХУ Фитнес клуб")]
        Element50 = 49,

        [Display(Name = "Центральный склад № 1/1")]
        Element51 = 50,

        [Display(Name = "Центральный склад №2")]
        Element52 = 51,

        [Display(Name = "Центральный склад №3")]
        Element53 = 52,

        [Display(Name = "Центральный склад ГСМ")]
        Element54 = 53
    }

}