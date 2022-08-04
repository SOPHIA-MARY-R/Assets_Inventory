namespace Fluid.Shared.Enums
{
    public enum DriveBusType : byte
    {
        Unknown = 0,
        SCSI = 1,
        ATAPI = 2,
        ATA = 3,
        IEEE1394 = 4,
        SSA = 5,
        FibreChannel = 6,
        USB = 7,
        RAID = 8,
        iSCSI = 9,
        SAS = 10,
        SATA = 11,
        SD = 12,
        MMC = 13,
        MAX = 14,
        FileBackedVirtual = 15,
        StorageSpaces = 16,
        NVMe = 17,
        MicrosoftReserved = 18,
    }
}
