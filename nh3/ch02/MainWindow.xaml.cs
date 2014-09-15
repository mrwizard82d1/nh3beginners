using System.Windows;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ch02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private const string ConnectionString =
            "server=SCDEV02;database=nh3beginners;Integrated Security=SSPI";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateDatabaseClicked(object sender, RoutedEventArgs e)
        {
            Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2012.ConnectionString(
                            ConnectionString))
                    .Mappings(
                        m =>
                            m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                    .ExposeConfiguration(CreateSchema)
                    .BuildConfiguration();
        }

        private static void CreateSchema(Configuration configuration)
        {
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }
    }
}
