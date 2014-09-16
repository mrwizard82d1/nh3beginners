using System.Diagnostics;
using System.Linq;
using System.Windows;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
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

        private static void CreateSchema(Configuration configuration)
        {
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                        .Database(
                            MsSqlConfiguration.MsSql2012.ConnectionString(
                                ConnectionString))
                        .Mappings(
                            m =>
                                m.FluentMappings
                                 .AddFromAssemblyOf<ProductMap>())
                        .BuildSessionFactory();
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

        private void CreateSessionFactoryClicked(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            Debug.Assert(factory != null);
        }

        private void OpenSessionClicked(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                Debug.Assert(session != null);
            }
        }

        private void AddCategoryClicked(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                var category = new Category
                {
                    Name = CategoryName.Text,
                    Description = CategoryDescription.Text
                };
                session.Save(category);
            }
        }

        private void LoadCategoriesClicked(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                var categories =
                    session.Query<Category>().OrderBy(c => c.Name);
                CategoryList.ItemsSource = categories;
                CategoryList.DisplayMemberPath = "Name";
            }
        }
    }
}
