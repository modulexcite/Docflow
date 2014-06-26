using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Attributes;

namespace RapidDoc.Models.Repository
{
    public enum DocumentState : byte
    {
        [LocalizedDescAttributre("Created", typeof(RapidDoc.Resources.Enums.Enums))]
        Created = 0,

        [LocalizedDescAttributre("Agreement", typeof(RapidDoc.Resources.Enums.Enums))]
        Agreement = 1,

        [LocalizedDescAttributre("Completed", typeof(RapidDoc.Resources.Enums.Enums))]
        Completed = 2,

        [LocalizedDescAttributre("Cancelled", typeof(RapidDoc.Resources.Enums.Enums))]
        Cancelled = 3,

        [LocalizedDescAttributre("Execution", typeof(RapidDoc.Resources.Enums.Enums))]
        Execution = 4,

        [LocalizedDescAttributre("Closed", typeof(RapidDoc.Resources.Enums.Enums))]
        Closed = 5
    }

    public enum TrackerType : byte
    {
        Waiting = 0,
        Approved = 1,
        Cancelled = 2,
        NonActive = 3
    }

    public enum SLAStatusList : byte
    {
        NoWarning = 0,
        Warning = 1,
        Disturbance = 2
    }

    public enum DateType : byte
    {
        [LocalizedDescAttributre("WorkingDay", typeof(RapidDoc.Resources.Enums.Enums))]
        WorkingDay = 0,

        [LocalizedDescAttributre("DayOff", typeof(RapidDoc.Resources.Enums.Enums))]
        DayOff = 1,
    }

    public enum EmailTemplateType : byte
    {
        Default = 0,
        Comment = 1,
        Delegation = 2
    }

    public enum HistoryType : byte
    {
        NewDocument = 0,
        ApproveDocument = 1,
        CancelledDocument = 2,
        NewComment = 3
    }

    //Custom
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
        [Display(Name = "Документ")]
        Document = 0,

        [Display(Name = "Подпись")]
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

        [Display(Name = "БЛОК ОБОГАЩЕНИЯ")]
        DeputyDirectorProduction = 7,

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
        Other = 8
    }

    public enum PriorityIncident : byte
    {
        [Display(Name = "Низкий")]
        Low = 0,

        [Display(Name = "Средний")]
        Middle = 1,

        [Display(Name = "Высокий")]
        Hight = 2,

        [Display(Name = "Критический")]
        Critical = 3,
    }
}