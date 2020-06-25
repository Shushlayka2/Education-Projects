using Microsoft.EntityFrameworkCore;
using RelationToGraph.Models;
using System.Configuration;

namespace RelationToGraph
{
    public partial class RelationalDBContext : DbContext
    {
        public RelationalDBContext()
        {
        }

        public RelationalDBContext(DbContextOptions<RelationalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aircrafts> Aircrafts { get; set; }
        public virtual DbSet<Airlines> Airlines { get; set; }
        public virtual DbSet<Airports> Airports { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Flights> Flights { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }

        // Unable to generate entity type for table 'public.flights_and_seats'. Please see the warning messages.
        // Unable to generate entity type for table 'public.routes_and_aircrafts'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["AviationRepository"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<Aircrafts>(entity =>
            {
                entity.HasKey(e => e.AircraftId);

                entity.ToTable("aircrafts");

                entity.Property(e => e.AircraftId).HasColumnName("aircraft_id");

                entity.Property(e => e.AircraftNum)
                    .IsRequired()
                    .HasColumnName("aircraft_num");

                entity.Property(e => e.AircraftType)
                    .IsRequired()
                    .HasColumnName("aircraft_type");

                entity.Property(e => e.Airline).HasColumnName("airline");

                entity.HasOne(d => d.AirlineNavigation)
                    .WithMany(p => p.Aircrafts)
                    .HasForeignKey(d => d.Airline)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("aircrafts_airline_fkey");
            });

            modelBuilder.Entity<Airlines>(entity =>
            {
                entity.HasKey(e => e.AirlineId);

                entity.ToTable("airlines");

                entity.Property(e => e.AirlineId).HasColumnName("airline_id");

                entity.Property(e => e.AirlineName)
                    .IsRequired()
                    .HasColumnName("airline_name");

                entity.Property(e => e.AirlineRating)
                    .HasColumnName("airline_rating")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AirlineSafety)
                    .HasColumnName("airline_safety")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AirlineSite).HasColumnName("airline_site");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Airlines)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("airlines_country_fkey");
            });

            modelBuilder.Entity<Airports>(entity =>
            {
                entity.HasKey(e => e.AirportId);

                entity.ToTable("airports");

                entity.Property(e => e.AirportId).HasColumnName("airport_id");

                entity.Property(e => e.AirportCode)
                    .IsRequired()
                    .HasColumnName("airport_code");

                entity.Property(e => e.AirportName)
                    .IsRequired()
                    .HasColumnName("airport_name");

                entity.Property(e => e.City).HasColumnName("city");

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("airports_city_fkey");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("cities");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("city_name");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("cities_country_fkey");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("countries");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<Flights>(entity =>
            {
                entity.HasKey(e => e.FlightId);

                entity.ToTable("flights");

                entity.Property(e => e.FlightId).HasColumnName("flight_id");

                entity.Property(e => e.Aircraft).HasColumnName("aircraft");

                entity.Property(e => e.DepartureDate)
                    .HasColumnName("departure_date")
                    .HasColumnType("date");

                entity.Property(e => e.DestinationDate)
                    .HasColumnName("destination_date")
                    .HasColumnType("date");

                entity.Property(e => e.HandLuggageWeight).HasColumnName("hand_luggage_weight");

                entity.Property(e => e.LuggageWeight).HasColumnName("luggage_weight");

                entity.Property(e => e.Route).HasColumnName("route");

                entity.HasOne(d => d.AircraftNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Aircraft)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("flights_aircraft_fkey");

                entity.HasOne(d => d.RouteNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Route)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("flights_route_fkey");
            });

            modelBuilder.Entity<Routes>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.ToTable("routes");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.AircraftType)
                    .IsRequired()
                    .HasColumnName("aircraft_type");

                entity.Property(e => e.Airline).HasColumnName("airline");

                entity.Property(e => e.BusinessClassPrice).HasColumnName("business_class_price");

                entity.Property(e => e.DeparturePoint).HasColumnName("departure_point");

                entity.Property(e => e.DepartureTime)
                    .HasColumnName("departure_time")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.DestinationPoint).HasColumnName("destination_point");

                entity.Property(e => e.DestinationTime)
                    .HasColumnName("destination_time")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.EconomyClassPrice).HasColumnName("economy_class_price");

                entity.Property(e => e.FirstClassPrice).HasColumnName("first_class_price");

                entity.Property(e => e.RouteCode)
                    .IsRequired()
                    .HasColumnName("route_code");

                entity.HasOne(d => d.AirlineNavigation)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.Airline)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("routes_airline_fkey");

                entity.HasOne(d => d.DeparturePointNavigation)
                    .WithMany(p => p.RoutesDeparturePointNavigation)
                    .HasForeignKey(d => d.DeparturePoint)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("routes_departure_point_fkey");

                entity.HasOne(d => d.DestinationPointNavigation)
                    .WithMany(p => p.RoutesDestinationPointNavigation)
                    .HasForeignKey(d => d.DestinationPoint)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("routes_destination_point_fkey");
            });

            modelBuilder.Entity<Seats>(entity =>
            {
                entity.HasKey(e => e.SeatId);

                entity.ToTable("seats");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.Aircraft).HasColumnName("aircraft");

                entity.Property(e => e.SeatNum)
                    .IsRequired()
                    .HasColumnName("seat_num");

                entity.Property(e => e.SeatType)
                    .IsRequired()
                    .HasColumnName("seat_type");

                entity.HasOne(d => d.AircraftNavigation)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.Aircraft)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("seats_aircraft_fkey");
            });
        }
    }
}
