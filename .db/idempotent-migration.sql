CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE TABLE "Owners" (
        "OwnerId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Address" text NOT NULL,
        "Photo" text NOT NULL,
        "Birthday" timestamp with time zone NOT NULL,
        "CreatedBy" text NOT NULL,
        "LastUpdatedBy" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        "LastUpdatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        CONSTRAINT "PK_Owners" PRIMARY KEY ("OwnerId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE TABLE "Properties" (
        "PropertyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Address" text NOT NULL,
        "Price" numeric NOT NULL,
        "CodeInternal" text NOT NULL,
        "Year" integer NOT NULL,
        "OwnerId" uuid NOT NULL,
        "CreatedBy" text NOT NULL,
        "LastUpdatedBy" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        "LastUpdatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        CONSTRAINT "PK_Properties" PRIMARY KEY ("PropertyId"),
        CONSTRAINT "FK_Properties_Owners_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES "Owners" ("OwnerId") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE TABLE "PropertyImages" (
        "PropertyImageId" uuid NOT NULL,
        "PropertyId" uuid NOT NULL,
        "File" text NOT NULL,
        "Enabled" integer NOT NULL,
        "CreatedBy" text NOT NULL,
        "LastUpdatedBy" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        "LastUpdatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        CONSTRAINT "PK_PropertyImages" PRIMARY KEY ("PropertyImageId"),
        CONSTRAINT "FK_PropertyImages_Properties_PropertyId" FOREIGN KEY ("PropertyId") REFERENCES "Properties" ("PropertyId") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE TABLE "PropertyTraces" (
        "PropertyTraceId" uuid NOT NULL,
        "DateSale" timestamp with time zone NOT NULL,
        "Name" text NOT NULL,
        "Value" numeric NOT NULL,
        "Tax" numeric NOT NULL,
        "PropertyId" uuid NOT NULL,
        "CreatedBy" text NOT NULL,
        "LastUpdatedBy" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        "LastUpdatedAt" timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
        CONSTRAINT "PK_PropertyTraces" PRIMARY KEY ("PropertyTraceId"),
        CONSTRAINT "FK_PropertyTraces_Properties_PropertyId" FOREIGN KEY ("PropertyId") REFERENCES "Properties" ("PropertyId") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    INSERT INTO "Owners" ("OwnerId", "Address", "Birthday", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "Photo")
    VALUES ('00000000-0000-0000-0000-000000000001', '123 Main St, Springfield', TIMESTAMPTZ '1980-05-20T05:00:00Z', TIMESTAMPTZ '2024-09-19T20:28:13.904518+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.904518+00:00', '', 'John Doe', 'johndoe.jpg');
    INSERT INTO "Owners" ("OwnerId", "Address", "Birthday", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "Photo")
    VALUES ('00000000-0000-0000-0000-000000000002', '456 Oak Ave, Springfield', TIMESTAMPTZ '1985-03-15T05:00:00Z', TIMESTAMPTZ '2024-09-19T20:28:13.904519+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.904519+00:00', '', 'Jane Smith', 'janesmith.jpg');
    INSERT INTO "Owners" ("OwnerId", "Address", "Birthday", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "Photo")
    VALUES ('00000000-0000-0000-0000-000000000003', '789 Pine Rd, Springfield', TIMESTAMPTZ '1990-11-10T05:00:00Z', TIMESTAMPTZ '2024-09-19T20:28:13.904519+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.904519+00:00', '', 'Robert Brown', 'robertbrown.jpg');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000101', '123 Country Road, Springfield', 'GR12345', TIMESTAMPTZ '2024-09-19T20:28:13.905371+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905371+00:00', '', 'Green Acres', '00000000-0000-0000-0000-000000000001', 250000.0, 2010);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000102', '456 Beach Blvd, Springfield', 'OV45678', TIMESTAMPTZ '2024-09-19T20:28:13.905371+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905371+00:00', '', 'Ocean View', '00000000-0000-0000-0000-000000000002', 450000.0, 2015);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000103', '789 Hilltop Dr, Springfield', 'MR78910', TIMESTAMPTZ '2024-09-19T20:28:13.905372+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905372+00:00', '', 'Mountain Retreat', '00000000-0000-0000-0000-000000000003', 350000.0, 2012);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000104', '101 Downtown Ave, Springfield', 'CL10111', TIMESTAMPTZ '2024-09-19T20:28:13.905372+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905372+00:00', '', 'City Lights', '00000000-0000-0000-0000-000000000001', 600000.0, 2020);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000105', '202 Suburb Ln, Springfield', 'SD20222', TIMESTAMPTZ '2024-09-19T20:28:13.905374+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905374+00:00', '', 'Suburban Dream', '00000000-0000-0000-0000-000000000002', 300000.0, 2005);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000106', '303 City Center Rd, Springfield', 'DL30333', TIMESTAMPTZ '2024-09-19T20:28:13.905374+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905375+00:00', '', 'Downtown Loft', '00000000-0000-0000-0000-000000000003', 450000.0, 2018);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000107', '404 Rural Dr, Springfield', 'CE40444', TIMESTAMPTZ '2024-09-19T20:28:13.905375+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905375+00:00', '', 'Countryside Estate', '00000000-0000-0000-0000-000000000001', 700000.0, 2017);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000108', '505 Lakeside Rd, Springfield', 'LH50555', TIMESTAMPTZ '2024-09-19T20:28:13.905375+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905376+00:00', '', 'Lakehouse', '00000000-0000-0000-0000-000000000002', 800000.0, 2019);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000109', '606 High Rise Blvd, Springfield', 'PH60666', TIMESTAMPTZ '2024-09-19T20:28:13.905376+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905376+00:00', '', 'Penthouse Suite', '00000000-0000-0000-0000-000000000003', 950000.0, 2021);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000110', '707 Farm Ln, Springfield', 'CC70777', TIMESTAMPTZ '2024-09-19T20:28:13.905376+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905376+00:00', '', 'Country Cottage', '00000000-0000-0000-0000-000000000001', 200000.0, 2000);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000111', '808 Palm Ave, Springfield', 'LV80888', TIMESTAMPTZ '2024-09-19T20:28:13.905377+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905377+00:00', '', 'Luxury Villa', '00000000-0000-0000-0000-000000000002', 1200000.0, 2022);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000112', '909 River Rd, Springfield', 'BB90999', TIMESTAMPTZ '2024-09-19T20:28:13.905377+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905377+00:00', '', 'Bungalow Bliss', '00000000-0000-0000-0000-000000000003', 275000.0, 2008);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000113', '1001 Woods Ln, Springfield', 'FL101010', TIMESTAMPTZ '2024-09-19T20:28:13.905378+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905378+00:00', '', 'Forest Lodge', '00000000-0000-0000-0000-000000000001', 650000.0, 2016);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000114', '1102 Coastal Rd, Springfield', 'SC111011', TIMESTAMPTZ '2024-09-19T20:28:13.905378+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.905378+00:00', '', 'Seaside Cottage', '00000000-0000-0000-0000-000000000002', 300000.0, 2011);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000115', '1203 Metro Blvd, Springfield', 'US121212', TIMESTAMPTZ '2024-09-19T20:28:13.90538+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.90538+00:00', '', 'Urban Studio', '00000000-0000-0000-0000-000000000003', 350000.0, 2014);
    INSERT INTO "Properties" ("PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year")
    VALUES ('00000000-0000-0000-0000-000000000116', '1304 Valley Rd, Springfield', 'HM131313', TIMESTAMPTZ '2024-09-19T20:28:13.90538+00:00', '', TIMESTAMPTZ '2024-09-19T20:28:13.90538+00:00', '', 'Hillside Manor', '00000000-0000-0000-0000-000000000001', 500000.0, 2023);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000101', TIMESTAMPTZ '2024-09-19T20:28:13.905858+00:00', '', 1, 'green_acres_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905858+00:00', '', '00000000-0000-0000-0000-000000000101');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000102', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', 1, 'ocean_view_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', '00000000-0000-0000-0000-000000000102');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000103', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', 1, 'mountain_retreat_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', '00000000-0000-0000-0000-000000000103');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000104', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', 1, 'city_lights_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905859+00:00', '', '00000000-0000-0000-0000-000000000104');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000105', TIMESTAMPTZ '2024-09-19T20:28:13.90586+00:00', '', 1, 'suburban_dream_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.90586+00:00', '', '00000000-0000-0000-0000-000000000105');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000106', TIMESTAMPTZ '2024-09-19T20:28:13.90586+00:00', '', 1, 'downtown_loft_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.90586+00:00', '', '00000000-0000-0000-0000-000000000106');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000107', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', 1, 'countryside_estate_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', '00000000-0000-0000-0000-000000000107');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000108', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', 1, 'lakehouse_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', '00000000-0000-0000-0000-000000000108');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000109', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', 1, 'penthouse_suite_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905861+00:00', '', '00000000-0000-0000-0000-000000000109');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000110', TIMESTAMPTZ '2024-09-19T20:28:13.905862+00:00', '', 1, 'country_cottage_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905862+00:00', '', '00000000-0000-0000-0000-000000000110');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000111', TIMESTAMPTZ '2024-09-19T20:28:13.905862+00:00', '', 1, 'luxury_villa_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905862+00:00', '', '00000000-0000-0000-0000-000000000111');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000112', TIMESTAMPTZ '2024-09-19T20:28:13.905867+00:00', '', 1, 'bungalow_bliss_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905867+00:00', '', '00000000-0000-0000-0000-000000000112');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000113', TIMESTAMPTZ '2024-09-19T20:28:13.905868+00:00', '', 1, 'forest_lodge_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905868+00:00', '', '00000000-0000-0000-0000-000000000113');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000114', TIMESTAMPTZ '2024-09-19T20:28:13.905868+00:00', '', 1, 'seaside_cottage_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905868+00:00', '', '00000000-0000-0000-0000-000000000114');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000115', TIMESTAMPTZ '2024-09-19T20:28:13.905869+00:00', '', 1, 'urban_studio_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905869+00:00', '', '00000000-0000-0000-0000-000000000115');
    INSERT INTO "PropertyImages" ("PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId")
    VALUES ('00000000-0000-0000-0000-000000000116', TIMESTAMPTZ '2024-09-19T20:28:13.905869+00:00', '', 1, 'hillside_manor_1.jpg', TIMESTAMPTZ '2024-09-19T20:28:13.905869+00:00', '', '00000000-0000-0000-0000-000000000116');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_Owners_CreatedAt" ON "Owners" ("CreatedAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_Properties_CreatedAt" ON "Properties" ("CreatedAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_Properties_OwnerId" ON "Properties" ("OwnerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_PropertyImages_CreatedAt" ON "PropertyImages" ("CreatedAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_PropertyImages_PropertyId" ON "PropertyImages" ("PropertyId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_PropertyTraces_CreatedAt" ON "PropertyTraces" ("CreatedAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    CREATE INDEX "IX_PropertyTraces_PropertyId" ON "PropertyTraces" ("PropertyId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240919202814_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240919202814_InitialCreate', '8.0.8');
    END IF;
END $EF$;
COMMIT;

