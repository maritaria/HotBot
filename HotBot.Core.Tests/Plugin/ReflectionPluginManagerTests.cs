using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Plugins.Tests
{
	[TestClass()]
	public class ReflectionPluginManagerTests
	{
		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null()
		{
			var manager = new ReflectionPluginManager(null);
		}

		[TestMethod()]
		public void Constructor_Valid()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			Assert.AreEqual(container.Object, manager.Container, "Container property invalid");
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddPlugin_Null()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.AddPlugin(null);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void AddPlugin_NoDescription()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = new Mock<Plugin>();
			manager.AddPlugin(plugin.Object);
		}

		[TestMethod()]
		public void AddPlugin_Valid()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
		}

		[TestMethod()]
		public void AddPlugin_AddTwice()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.AddPlugin(plugin.Object);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void AddPlugin_NameCollision()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin1 = CreatePlugin<SubPlugin1>("test");
			var plugin2 = CreatePlugin<SubPlugin2>("test");
			manager.AddPlugin(plugin1.Object);
			manager.AddPlugin(plugin2.Object);
		}

		public interface SubPlugin1 : Plugin { }

		public interface SubPlugin2 : Plugin { }

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RemovePlugin_Null()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.RemovePlugin(null);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void RemovePlugin_NoDescription()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = new Mock<Plugin>();
			manager.RemovePlugin(plugin.Object);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetPlugin_Named_Null()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			string s = null;
			manager.GetPlugin(s);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void GetPlugin_Named_EmptyString()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.GetPlugin(string.Empty);
		}

		[TestMethod()]
		public void GetPlugin_Named_Valid()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			Assert.AreEqual(plugin.Object, manager.GetPlugin("test"));
		}

		[TestMethod()]
		public void GetPlugin_Named_NoPlugin()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("q");
			manager.AddPlugin(plugin.Object);
			Assert.IsNull(manager.GetPlugin("test"));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetPlugin_Typed_Null()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			Type type = null;
			manager.GetPlugin(type);
		}

		[TestMethod()]
		public void GetPlugin_Typed_Valid()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			Assert.AreEqual(plugin.Object, manager.GetPlugin(plugin.Object.GetType()));
		}

		[TestMethod()]
		public void GetPlugin_Typed_NoPlugin()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			Assert.AreEqual(plugin.Object, manager.GetPlugin(plugin.Object.GetType()));
			Assert.IsNull(manager.GetPlugin(typeof(SubPlugin1)));
		}

		[TestMethod()]
		public void GetPluginTest_Generics()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = new PluginImpl { Description = new PluginImpl.DescriptionImpl { Name = "test" } };
			manager.AddPlugin(plugin);
			Assert.AreEqual(plugin, manager.GetPlugin<PluginImpl>());
		}

		private class PluginImpl : Plugin
		{
			public PluginDescription Description { get; set; }

			public PluginManager Manager { get; set; }

			public int Loads = 0;
			public int Unloads = 0;

			public void Load(IUnityContainer container)
			{
				Loads++;
			}

			public void Unload()
			{
				Unloads++;
			}

			internal class DescriptionImpl : PluginDescription
			{
				public IReadOnlyCollection<Dependency> Dependencies { get; set; }

				public string Description { get; set; }

				public string Name { get; set; }
			}
		}

		[TestMethod()]
		public void GetPluginTest_Generics_NoPlugin()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			Assert.IsNull(manager.GetPlugin<SubPlugin1>());
		}

		[TestMethod()]
		public void Startup_CallLoadOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.Startup();
			plugin.Verify(p => p.Load(It.Is<IUnityContainer>(c => true)), Times.Once());
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Startup_ErrorWhenActive()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.Startup();
			manager.Startup();
		}

		[TestMethod()]
		public void AddPlugin_DontAutostart()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.Startup();
			var plugin = CreatePlugin<Plugin>("test");
			plugin.Setup(p => p.Load(null)).Callback(Assert.Fail);
			manager.AddPlugin(plugin.Object);
		}

		[TestMethod()]
		public void Shutdown_CallLoadOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.Startup();
			manager.Shutdown();
			plugin.Verify(p => p.Unload(), Times.Once());
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Shutdown_ErrorWhenInactive()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.Shutdown();
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Shutdown_Repeat()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.Startup();
			manager.Shutdown();
			manager.Shutdown();
		}

		[TestMethod()]
		public void Restart_LoadAndOnloadOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.Startup();
			var plugin = CreatePlugin<Plugin>("test");
			bool loaded = false;
			bool unloaded = false;

			plugin.Setup(p => p.Load(null)).Callback(() =>
			{
				Assert.IsTrue(unloaded, "Load() called before Unload()");
				Assert.IsFalse(loaded, "Load() called multiple times");
				loaded = true;
			});
			plugin.Setup(p => p.Unload()).Callback(() =>
			{
				Assert.IsFalse(unloaded, "Unload() called multiple times");
				unloaded = true;
			});
			manager.AddPlugin(plugin.Object);

			manager.Restart();
		}

		private static Mock<TPlugin> CreatePlugin<TPlugin>(string name) where TPlugin : class, Plugin
		{
			var description = new Mock<PluginDescription>();
			description.SetupGet(d => d.Name).Returns(name);
			var plugin = new Mock<TPlugin>();
			plugin.SetupGet(p => p.Description).Returns(description.Object);
			return plugin;
		}
	}
}