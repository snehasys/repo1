﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum TagType
    {
        ActivationDate = 0x420001,
        ApplicationData = 0x420002,
        ApplicationNamespace = 0x420003,
        ApplicationSpecificInformation = 0x420004,
        ArchiveDate = 0x420005,
        AsynchronousCorrelationValue = 0x420006,
        AsynchronousIndicator = 0x420007,
        Attribute = 0x420008,
        AttributeIndex = 0x420009,
        AttributeName = 0x42000A,
        AttributeValue = 0x42000B,
        Authentication = 0x42000C,
        BatchCount = 0x42000D,
        BatchErrorContinuationOption = 0x42000E,
        BatchItem = 0x42000F,
        BatchOrderOption = 0x420010,
        BlockCipherMode = 0x420011,
        CancellationResult = 0x420012,
        Certificate = 0x420013,
        CertificateIdentifier = 0x420014,
        CertificateIssuer = 0x420015,
        CertificateIssuerAlternativeName = 0x420016,
        CertificateIssuerDistinguishedName = 0x420017,
        CertificateRequest = 0x420018,
        CertificateRequestType = 0x420019,
        CertificateSubject = 0x42001A,
        CertificateSubjectAlternativeName = 0x42001B,
        CertificateSubjectDistinguishedName = 0x42001C,
        CertificateType = 0x42001D,
        CertificateValue = 0x42001E,
        CommonTemplateAttribute = 0x42001F,
        CompromiseDate = 0x420020,
        CompromiseOccurrenceDate = 0x420021,
        ContactInformation = 0x420022,
        Credential = 0x420023,
        CredentialType = 0x420024,
        CredentialValue = 0x420025,
        CriticalityIndicator = 0x420026,
        CRTCoefficient = 0x420027,
        CryptographicAlgorithm = 0x420028,
        CryptographicDomainParameters = 0x420029,
        CryptographicLength = 0x42002A,
        CryptographicParameters = 0x42002B,
        CryptographicUsageMask = 0x42002C,
        CustomAttribute = 0x42002D,
        D = 0x42002E,
        DeactivationDate = 0x42002F,
        DerivationData = 0x420030,
        DerivationMethod = 0x420031,
        DerivationParameters = 0x420032,
        DestroyDate = 0x420033,
        Digest = 0x420034,
        DigestValue = 0x420035,
        EncryptionKeyInformation = 0x420036,
        G = 0x420037,
        HashingAlgorithm = 0x420038,
        InitialDate = 0x420039,
        InitializationVector = 0x42003A,
        Issuer = 0x42003B,
        IterationCount = 0x42003C,
        IVCounterNonce = 0x42003D,
        J = 0x42003E,
        Key = 0x42003F,
        KeyBlock = 0x420040,
        KeyCompressionType = 0x420041,
        KeyFormatType = 0x420042,
        KeyMaterial = 0x420043,
        KeyPartIdentifier = 0x420044,
        KeyValue = 0x420045,
        KeyWrappingData = 0x420046,
        KeyWrappingSpecification = 0x420047,
        LastChangeDate = 0x420048,
        LeaseTime = 0x420049,
        Link = 0x42004A,
        LinkType = 0x42004B,
        LinkedObjectIdentifier = 0x42004C,
        MACSignature = 0x42004D,
        MACSignatureKeyInformation = 0x42004E,
        MaximumItems = 0x42004F,
        MaximumResponseSize = 0x420050,
        MessageExtension = 0x420051,
        Modulus = 0x420052,
        Name = 0x420053,
        NameType = 0x420054,
        NameValue = 0x420055,
        ObjectGroup = 0x420056,
        ObjectType = 0x420057,
        Offset = 0x420058,
        OpaqueDataType = 0x420059,
        OpaqueDataValue = 0x42005A,
        OpaqueObject = 0x42005B,
        Operation = 0x42005C,
        OperationPolicyName = 0x42005D,
        P = 0x42005E,
        PaddingMethod = 0x42005F,
        PrimeExponentP = 0x420060,
        PrimeExponentQ = 0x420061,
        PrimeFieldSize = 0x420062,
        PrivateExponent = 0x420063,
        PrivateKey = 0x420064,
        PrivateKeyTemplateAttribute = 0x420065,
        PrivateKeyUniqueIdentifier = 0x420066,
        ProcessStartDate = 0x420067,
        ProtectStopDate = 0x420068,
        ProtocolVersion = 0x420069,
        ProtocolVersionMajor = 0x42006A,
        ProtocolVersionMinor = 0x42006B,
        PublicExponent = 0x42006C,
        PublicKey = 0x42006D,
        PublicKeyTemplateAttribute = 0x42006E,
        PublicKeyUniqueIdentifier = 0x42006F,
        PutFunction = 0x420070,
        Q = 0x420071,
        QString = 0x420072,
        Qlength = 0x420073,
        QueryFunction = 0x420074,
        RecommendedCurve = 0x420075,
        ReplacedUniqueIdentifier = 0x420076,
        RequestHeader = 0x420077,
        RequestMessage = 0x420078,
        RequestPayload = 0x420079,
        ResponseHeader = 0x42007A,
        ResponseMessage = 0x42007B,
        ResponsePayload = 0x42007C,
        ResultMessage = 0x42007D,
        ResultReason = 0x42007E,
        ResultStatus = 0x42007F,
        RevocationMessage = 0x420080,
        RevocationReason = 0x420081,
        RevocationReasonCode = 0x420082,
        KeyRoleType = 0x420083,
        Salt = 0x420084,
        SecretData = 0x420085,
        SecretDataType = 0x420086,
        SerialNumber = 0x420087,
        ServerInformation = 0x420088,
        SplitKey = 0x420089,
        SplitKeyMethod = 0x42008A,
        SplitKeyParts = 0x42008B,
        SplitKeyThreshold = 0x42008C,
        State = 0x42008D,
        StorageStatusMask = 0x42008E,
        SymmetricKey = 0x42008F,
        Template = 0x420090,
        TemplateAttribute = 0x420091,
        TimeStamp = 0x420092,
        UniqueBatchItemID = 0x420093,
        UniqueIdentifier = 0x420094,
        UsageLimits = 0x420095,
        UsageLimitsCount = 0x420096,
        UsageLimitsTotal = 0x420097,
        UsageLimitsUnit = 0x420098,
        Username = 0x420099,
        ValidityDate = 0x42009A,
        ValidityIndicator = 0x42009B,
        VendorExtension = 0x42009C,
        VendorIdentification = 0x42009D,
        WrappingMethod = 0x42009E,
        X = 0x42009F,
        Y = 0x4200A0,
        Password = 0x4200A1

    }
}
