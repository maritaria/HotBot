using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Plugins.Tests
{
	[TestClass()]
	public class ReflectionPluginManagerTests
	{
		[TestMethod()]
		public void ReflectionPluginManager_Constructor()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);

			TestUtils.AssertArgumentException(() => new ReflectionPluginManager(null));

			Assert.AreEqual(container.Object, manager.Container, "Container property invalid");
		}

		[TestMethod()]
		public void AddPlugin()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			var invalidPlugin = CreateInvalidPlugin();

			TestUtils.AssertArgumentException(() => manager.AddPlugin(null));
			TestUtils.AssertArgumentException(() => manager.AddPlugin(invalidPlugin.Object));

			manager.AddPlugin(plugin.Object);
		}

		[TestMethod()]
		public void AddPlugin_AddTwice()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			TestUtils.AssertException<InvalidOperationException>(() => manager.AddPlugin(plugin.Object));
		}

		[TestMethod()]
		public void AddPlugin_NameCollision()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin1 = CreatePlugin<UniquePluginType1>("test");
			var plugin2 = CreatePlugin<UniquePluginType2>("test");
			manager.AddPlugin(plugin1.Object);
			TestUtils.AssertException<InvalidOperationException>(() => manager.AddPlugin(plugin2.Object));
		}

		[TestMethod()]
		public void AddPlugin_DontAutostart()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.LoadAll();
			var plugin = CreatePlugin<Plugin>("test");
			plugin.Setup(p => p.Load(null)).Callback(Assert.Fail);
			manager.AddPlugin(plugin.Object);
		}

		public interface UniquePluginType1 : Plugin { }

		public interface UniquePluginType2 : Plugin { }

		[TestMethod()]
		public void RemovePlugin()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var invalidPlugin = CreateInvalidPlugin();
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);

			TestUtils.AssertArgumentException(() => manager.RemovePlugin(null));
			TestUtils.AssertArgumentException(() => manager.RemovePlugin(invalidPlugin.Object));

			manager.RemovePlugin(plugin.Object);
		}

		[TestMethod()]
		public void GetPlugin_Named()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);

			TestUtils.AssertArgumentException(() => manager.GetPlugin((string)null));
			TestUtils.AssertArgumentException(() => manager.GetPlugin(""));

			Assert.AreEqual(plugin.Object, manager.GetPlugin("test"));
			Assert.IsNull(manager.GetPlugin("q"));
		}

		[TestMethod()]
		public void GetPlugin_Typed()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<UniquePluginType1>("test");
			manager.AddPlugin(plugin.Object);

			TestUtils.AssertArgumentException(() => manager.GetPlugin((Type)null));

			Assert.AreEqual(plugin.Object, manager.GetPlugin(plugin.Object.GetType()));
			Assert.IsNull(manager.GetPlugin(typeof(Plugin)));
			Assert.IsNull(manager.GetPlugin(typeof(UniquePluginType2)));
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
		}

		[TestMethod()]
		public void LoadAll_CallOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			var newPlugin = CreatePlugin<UniquePluginType1>("test2");

			manager.AddPlugin(plugin.Object);
			manager.LoadAll();

			plugin.Verify(p => p.Load(It.Is<IUnityContainer>(c => true)), Times.Once());

			manager.AddPlugin(newPlugin.Object);
			manager.LoadAll();

			plugin.Verify(p => p.Load(It.Is<IUnityContainer>(c => true)), Times.Once());
			newPlugin.Verify(p => p.Load(It.Is<IUnityContainer>(c => true)), Times.Once());
		}

		[TestMethod()]
		public void UnloadAll_CallOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.AddPlugin(plugin.Object);
			manager.LoadAll();
			manager.UnloadAll();

			plugin.Verify(p => p.Unload(), Times.Once());

			manager.LoadAll();
			manager.RemovePlugin(plugin.Object);
			manager.UnloadAll();

			plugin.Verify(p => p.Unload(), Times.Once());
		}

		[TestMethod()]
		public void Reload_LoadAndOnloadOnce()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			manager.LoadAll();
			var plugin = CreatePlugin<Plugin>("test");
			bool loaded = false;
			bool unloaded = false;

			plugin.Setup(p => p.Load(container.Object)).Callback(() =>
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
			manager.Reload();
		}

		[TestMethod()]
		public void Reload_DontLoadUnloadedPlugins()
		{
			var container = new Mock<IUnityContainer>();
			var manager = new ReflectionPluginManager(container.Object);
			var plugin = CreatePlugin<Plugin>("test");
			manager.LoadAll();
			manager.AddPlugin(plugin.Object);
			manager.Reload();
			plugin.Verify(p => p.Load(container.Object), Times.Never());
		}

		private static Mock<TPlugin> CreatePlugin<TPlugin>(string name) where TPlugin : class, Plugin
		{
			var description = new PluginDescription(name, "");
			var plugin = new Mock<TPlugin>();
			plugin.SetupGet(p => p.Description).Returns(description);
			return plugin;
		}

		private static Mock<Plugin> CreateInvalidPlugin()
		{
			return new Mock<Plugin>();
		}
	}
}