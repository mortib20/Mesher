using Microsoft.Extensions.Logging;

namespace Mesher.Database.Patch.Implementation;

public class MeshHardwarePatch(ILogger<MeshHardwarePatch> logger) : IMeshPatch<MeshHardwarePatch>
{
    public async Task ApplyPatch(MesherContext mesherContext, CancellationToken cancellationToken = default)
    {
        using var logScope = logger.BeginScope("MeshHardwarePatch");
        var dbSetMeshHardware = mesherContext.MeshHardwares;

        // TODO Put list into a config
        // HardwareModel
        // https://github.com/meshtastic/protobufs/blob/7eb3258fa06d7c5e5a32564b4c5b38326640c796/meshtastic/mesh.proto
        var meshHardware = new List<DbMeshHardware>
        {
            new() { Key = 0, Name = "UNSET" },
            new() { Key = 1, Name = "TLORA_V2" },
            new() { Key = 2, Name = "TLORA_V1" },
            new() { Key = 3, Name = "TLORA_V2_1_1P6" },
            new() { Key = 4, Name = "TBEAM" },
            new() { Key = 5, Name = "HELTEC_V2_0" },
            new() { Key = 6, Name = "TBEAM_V0P7" },
            new() { Key = 7, Name = "T_ECHO" },
            new() { Key = 8, Name = "TLORA_V1_1P3" },
            new() { Key = 9, Name = "RAK4631" },
            new() { Key = 10, Name = "HELTEC_V2_1" },
            new() { Key = 11, Name = "HELTEC_V1" },
            new() { Key = 12, Name = "LILYGO_TBEAM_S3_CORE" },
            new() { Key = 13, Name = "RAK11200" },
            new() { Key = 14, Name = "NANO_G1" },
            new() { Key = 15, Name = "TLORA_V2_1_1P8" },
            new() { Key = 16, Name = "TLORA_T3_S3" },
            new() { Key = 17, Name = "NANO_G1_EXPLORER" },
            new() { Key = 18, Name = "NANO_G2_ULTRA" },
            new() { Key = 19, Name = "LORA_TYPE" },
            new() { Key = 20, Name = "WIPHONE" },
            new() { Key = 21, Name = "WIO_WM1110" },
            new() { Key = 22, Name = "RAK2560" },
            new() { Key = 23, Name = "HELTEC_HRU_3601" },
            new() { Key = 24, Name = "HELTEC_WIRELESS_BRIDGE" },
            new() { Key = 25, Name = "STATION_G1" },
            new() { Key = 26, Name = "RAK11310" },
            new() { Key = 27, Name = "SENSELORA_RP2040" },
            new() { Key = 28, Name = "SENSELORA_S3" },
            new() { Key = 29, Name = "CANARYONE" },
            new() { Key = 30, Name = "RP2040_LORA" },
            new() { Key = 31, Name = "STATION_G2" },
            new() { Key = 32, Name = "LORA_RELAY_V1" },
            new() { Key = 33, Name = "NRF52840DK" },
            new() { Key = 34, Name = "PPR" },
            new() { Key = 35, Name = "GENIEBLOCKS" },
            new() { Key = 36, Name = "NRF52_UNKNOWN" },
            new() { Key = 37, Name = "PORTDUINO" },
            new() { Key = 38, Name = "ANDROID_SIM" },
            new() { Key = 39, Name = "DIY_V1" },
            new() { Key = 40, Name = "NRF52840_PCA10059" },
            new() { Key = 41, Name = "DR_DEV" },
            new() { Key = 42, Name = "M5STACK" },
            new() { Key = 43, Name = "HELTEC_V3" },
            new() { Key = 44, Name = "HELTEC_WSL_V3" },
            new() { Key = 45, Name = "BETAFPV_2400_TX" },
            new() { Key = 46, Name = "BETAFPV_900_NANO_TX" },
            new() { Key = 47, Name = "RPI_PICO" },
            new() { Key = 48, Name = "HELTEC_WIRELESS_TRACKER" },
            new() { Key = 49, Name = "HELTEC_WIRELESS_PAPER" },
            new() { Key = 50, Name = "T_DECK" },
            new() { Key = 51, Name = "T_WATCH_S3" },
            new() { Key = 52, Name = "PICOMPUTER_S3" },
            new() { Key = 53, Name = "HELTEC_HT62" },
            new() { Key = 54, Name = "EBYTE_ESP32_S3" },
            new() { Key = 55, Name = "ESP32_S3_PICO" },
            new() { Key = 56, Name = "CHATTER_2" },
            new() { Key = 57, Name = "HELTEC_WIRELESS_PAPER_V1_0" },
            new() { Key = 58, Name = "HELTEC_WIRELESS_TRACKER_V1_0" },
            new() { Key = 59, Name = "UNPHONE" },
            new() { Key = 60, Name = "TD_LORAC" },
            new() { Key = 61, Name = "CDEBYTE_EORA_S3" },
            new() { Key = 62, Name = "TWC_MESH_V4" },
            new() { Key = 63, Name = "NRF52_PROMICRO_DIY" },
            new() { Key = 64, Name = "RADIOMASTER_900_BANDIT_NANO" },
            new() { Key = 65, Name = "HELTEC_CAPSULE_SENSOR_V3" },
            new() { Key = 66, Name = "HELTEC_VISION_MASTER_T190" },
            new() { Key = 67, Name = "HELTEC_VISION_MASTER_E213" },
            new() { Key = 68, Name = "HELTEC_VISION_MASTER_E290" },
            new() { Key = 69, Name = "HELTEC_MESH_NODE_T114" },
            new() { Key = 70, Name = "SENSECAP_INDICATOR" },
            new() { Key = 71, Name = "TRACKER_T1000_E" },
            new() { Key = 72, Name = "RAK3172" },
            new() { Key = 73, Name = "WIO_E5" },
            new() { Key = 74, Name = "RADIOMASTER_900_BANDIT" },
            new() { Key = 75, Name = "ME25LS01_4Y10TD" },
            new() { Key = 76, Name = "RP2040_FEATHER_RFM95" },
            new() { Key = 77, Name = "M5STACK_COREBASIC" },
            new() { Key = 78, Name = "M5STACK_CORE2" },
            new() { Key = 79, Name = "RPI_PICO2" },
            new() { Key = 80, Name = "M5STACK_CORES3" },
            new() { Key = 81, Name = "SEEED_XIAO_S3" },
            new() { Key = 82, Name = "MS24SF1" },
            new() { Key = 83, Name = "TLORA_C6" },
            new() { Key = 84, Name = "WISMESH_TAP" },
            new() { Key = 85, Name = "ROUTASTIC" },
            new() { Key = 86, Name = "MESH_TAB" },
            new() { Key = 87, Name = "MESHLINK" },
            new() { Key = 88, Name = "XIAO_NRF52_KIT" },
            new() { Key = 89, Name = "THINKNODE_M1" },
            new() { Key = 90, Name = "THINKNODE_M2" },
            new() { Key = 91, Name = "T_ETH_ELITE" },
            new() { Key = 92, Name = "HELTEC_SENSOR_HUB" },
            new() { Key = 93, Name = "RESERVED_FRIED_CHICKEN" },
            new() { Key = 94, Name = "HELTEC_MESH_POCKET" },
            new() { Key = 95, Name = "SEEED_SOLAR_NODE" },
            new() { Key = 96, Name = "NOMADSTAR_METEOR_PRO" },
            new() { Key = 97, Name = "CROWPANEL" },
            new() { Key = 98, Name = "LINK_32" },
            new() { Key = 99, Name = "SEEED_WIO_TRACKER_L1" },
            new() { Key = 100, Name = "SEEED_WIO_TRACKER_L1_EINK" },
            new() { Key = 101, Name = "MUZI_R1_NEO" },
            new() { Key = 102, Name = "T_DECK_PRO" },
            new() { Key = 103, Name = "T_LORA_PAGER" },
            new() { Key = 104, Name = "M5STACK_RESERVED" },
            new() { Key = 105, Name = "WISMESH_TAG" },
            new() { Key = 106, Name = "RAK3312" },
            new() { Key = 107, Name = "THINKNODE_M5" },
            new() { Key = 108, Name = "HELTEC_MESH_SOLAR" },
            new() { Key = 109, Name = "T_ECHO_LITE" },
            new() { Key = 110, Name = "HELTEC_V4" },
            new() { Key = 111, Name = "M5STACK_C6L" },
            new() { Key = 112, Name = "M5STACK_CARDPUTER_ADV" },
            new() { Key = 113, Name = "HELTEC_WIRELESS_TRACKER_V2" },
            new() { Key = 114, Name = "T_WATCH_ULTRA" },
            new() { Key = 115, Name = "THINKNODE_M3" },
            new() { Key = 116, Name = "WISMESH_TAP_V2" },
            new() { Key = 117, Name = "RAK3401" },
            new() { Key = 118, Name = "RAK6421" },
            new() { Key = 255, Name = "PRIVATE_HW" }
        };

        var hardwareInDb = dbSetMeshHardware.Select(s => s.Key).Distinct().ToList();
        var missingHardware = meshHardware.Where(s => !hardwareInDb.Contains(s.Key)).ToList();

        logger.LogInformation("Applying MeshHardwarePatch:");
        logger.LogInformation("Hardware in Database: {Count}", hardwareInDb.Count);
        logger.LogInformation("Found Hardware: {Count}", meshHardware.Count);
        if (hardwareInDb.Count != meshHardware.Count)
        {
            logger.LogInformation("Missing Hardware added to Database: {Devices}", string.Join(',', missingHardware.Select(s => s.Name)));
        }
        else
        {
            logger.LogInformation("Hardware in database is complete!");
            return;
        }
        
        // TODO remove of non existing

        await dbSetMeshHardware.AddRangeAsync(missingHardware, cancellationToken);

        await mesherContext.SaveChangesAsync(cancellationToken);
    }
}