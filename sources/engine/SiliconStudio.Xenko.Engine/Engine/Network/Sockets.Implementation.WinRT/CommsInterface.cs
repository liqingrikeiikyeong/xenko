// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
#if SILICONSTUDIO_PLATFORM_UWP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Sockets.Plugin.Abstractions;

namespace Sockets.Plugin
{
    /// <summary>
    /// Provides a summary of an available network interface on the device.
    /// </summary>
    class CommsInterface : ICommsInterface
    {
        /// <summary>
        /// The interface identifier provided by the underlying platform.
        /// </summary>
        public string NativeInterfaceId { get; private set; }

        /// <summary>
        /// The interface name, as provided by the underlying platform.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The IPv4 Address of the interface, if connected. 
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        /// The IPv4 address of the gateway, if available.
        /// </summary>
        public string GatewayAddress { get; private set; }

        /// <summary>
        /// The IPv4 broadcast address for the interface, if available.
        /// </summary>
        public string BroadcastAddress { get; private set; }

        /// <summary>
        /// Indicates whether the interface has a network address and can be used for 
        /// sending/receiving data.
        /// </summary>
        public bool IsUsable
        {
            get { return !String.IsNullOrWhiteSpace(IpAddress); }
        }

        private readonly string[] _loopbackAddresses = { "127.0.0.1", "localhost" };

        /// <summary>
        /// Indicates whether the interface is the loopback interface
        /// </summary>5
        public bool IsLoopback
        {
            // yes, crude.
            get { return _loopbackAddresses.Contains(IpAddress); }
        }

        /// <summary>
        /// The connection status of the interface, if available
        /// </summary>
        public CommsInterfaceStatus ConnectionStatus { get; private set; }

        /// <summary>
        /// The native HostName this CommsInterface represents.
        /// </summary>
        protected internal HostName NativeHostName;

        /// <summary>
        /// The native NetworkAdapter this CommsInterface represents.
        /// </summary>
        protected internal NetworkAdapter NativeNetworkAdapter;

        // TODO: Move to singleton, rather than static method?
        /// <summary>
        /// Retrieves information on the IPv4 network interfaces available.
        /// </summary>
        /// <returns></returns>
        public static Task<List<CommsInterface>> GetAllInterfacesAsync()
        {
            return Task.Run(() =>
            {
                var profiles = NetworkInformation
                    .GetConnectionProfiles()
                    .Where(cp => cp.NetworkAdapter != null)
                    .GroupBy(cp => cp.NetworkAdapter.NetworkAdapterId)
                    .Select(na => na.First())
                    .ToDictionary(cp => cp.NetworkAdapter.NetworkAdapterId.ToString(), cp => cp);

                var interfaces =
                    NetworkInformation
                        .GetHostNames()
                        .Where(hn => hn.IPInformation != null && hn.IPInformation.NetworkAdapter != null)
                        .Where(hn => hn.Type == HostNameType.Ipv4)
                        .Where(hn => hn.IPInformation.PrefixLength != null)
                        .Select(hn =>
                        {
                            var ipAddress = hn.CanonicalName;
                            var prefixLength = (int) hn.IPInformation.PrefixLength; // seriously why is this nullable

                            var subnetAddress = NetworkExtensions.GetSubnetAddress(ipAddress, prefixLength);
                            var broadcastAddress = NetworkExtensions.GetBroadcastAddress(ipAddress, subnetAddress);

                            var adapter = hn.IPInformation.NetworkAdapter;
                            var adapterId = adapter.NetworkAdapterId.ToString();
                            var adapterName = "{ unknown }";
                            ConnectionProfile matchingProfile;

                            if (profiles.TryGetValue(adapterId, out matchingProfile))
                                adapterName = matchingProfile.ProfileName;

                            return new CommsInterface
                            {
                                NativeInterfaceId = adapterId,
                                Name = adapterName,
                                IpAddress = ipAddress,
                                BroadcastAddress = broadcastAddress,
                                GatewayAddress = null,

                                NativeHostName = hn,
                                NativeNetworkAdapter = adapter,

                                ConnectionStatus = CommsInterfaceStatus.Unknown
                            };
                        }).ToList();

                return interfaces;
            });
        }
    }
}
#endif
