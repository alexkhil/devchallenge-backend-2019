namespace SC.DevChallenge.DataAccess.EF.Configurations
{
    //internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
    //{
    //    public void Configure(EntityTypeBuilder<Asset> builder)
    //    {
    //        builder.ToTable(Tables.Assets, Schemas.Dbo);

    //        builder.HasKey(a => a.Id);

    //        builder.Property(a => a.Name)
    //               .IsRequired()
    //               .HasMaxLength(128);

    //        builder.Property(a => a.Url)
    //               .IsRequired()
    //               .HasConversion(a => a.ToString(), a => new Uri(a));

    //        builder.HasMany(a => a.Studios)
    //               .WithOne(s => s.Logo)
    //               .HasForeignKey(s => s.LogoId);
    //    }
    //}
}