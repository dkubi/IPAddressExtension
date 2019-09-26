// MIT License

// Copyright (c) 2019 Daniel Kubis
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace IPAddressExtension {
    public static class IPAddressExtensions {
        private static readonly IPNetwork[] ReservedNetworks = {
            IPNetwork.Parse ("0.0.0.0/8"),
            IPNetwork.Parse ("10.0.0.0/8"),
            IPNetwork.Parse ("100.64.0.0/10"),
            IPNetwork.Parse ("127.0.0.0/8"),
            IPNetwork.Parse ("169.254.0.0/16"),
            IPNetwork.Parse ("172.16.0.0/12"),
            IPNetwork.Parse ("192.0.0.0/29"),
            IPNetwork.Parse ("192.0.2.0/24"),
            IPNetwork.Parse ("192.88.99.0/24"),
            IPNetwork.Parse ("192.168.0.0/16"),
            IPNetwork.Parse ("198.18.0.0/15"),
            IPNetwork.Parse ("198.51.100.0/24"),
            IPNetwork.Parse ("203.0.113.0/24"),
            IPNetwork.Parse ("224.0.0.0/4"),
            IPNetwork.Parse ("240.0.0.0/4"),
            IPNetwork.Parse ("255.255.255.255/32"),
            IPNetwork.Parse ("::/128"), //  ist die nicht spezifizierte Adresse. Sie darf keinem Host zugewiesen werden, sondern zeigt das Fehlen einer Adresse an.
            IPNetwork.Parse ("::1/128"), //  ist die Adresse des eigenen Standortes (loopback-Adresse, die in der Regel mit localhost verknüpft ist).
            //            IPNetwork.Parse("::ffff:0:0/96"), // IPv4 mapped
            //            IPNetwork.Parse("100::/64"),
            IPNetwork.Parse ("64:ff9b::/96"), // kann für den Übersetzungsmechanismus NAT64 gemäß RFC 6146 verwendet werden.
            //            IPNetwork.Parse("2001::/32"),
            //            IPNetwork.Parse("2001:10::/28"),
            IPNetwork.Parse ("2001:db8::/32"), // für Dokumentationszwecke
            //            IPNetwork.Parse("2002::/4"),// für den Tunnelmechanismus 6to4 deprecated
            IPNetwork.Parse ("fc00::/7"), // Für private Adressen gibt es die Unique Local Addresses (ULA), beschrieben in RFC 4193. 
            IPNetwork.Parse ("fe80::/10"), // Link-Local-Adressen[22] sind nur innerhalb abgeschlossener Netzwerksegmente gültig. Ein Netzwerksegment ist ein lokales Netz, gebildet mit Switches oder Hubs, bis zum ersten Router. Reserviert ist hierfür der Bereich „fe80::/10“.[23][24] Nach diesen 10 Bits folgen 54 Bits mit dem Wert 0, sodass die Link-Local-Adressen immer das Präfix „fe80::/64“ haben: 
            IPNetwork.Parse ("fe80::/64"), // Link-Local-Adressen[22] sind nur innerhalb abgeschlossener Netzwerksegmente gültig. Ein Netzwerksegment ist ein lokales Netz, gebildet mit Switches oder Hubs, bis zum ersten Router. Reserviert ist hierfür der Bereich „fe80::/10“.[23][24] Nach diesen 10 Bits folgen 54 Bits mit dem Wert 0, sodass die Link-Local-Adressen immer das Präfix „fe80::/64“ haben: 
            IPNetwork.Parse ("ff00::/8") //stehen für Multicast-Adressen.
        };

        public static bool IsReservedAddress (this IPAddress address) {
            return
            ReservedNetworks.Any (n => n.Contains (address)) ||
                address.AddressFamily == AddressFamily.InterNetwork && address.ToString ().EndsWith ("0") ||
                address.AddressFamily == AddressFamily.InterNetwork && address.ToString ().EndsWith ("255");
        }

        /// <summary>
        /// Check for reserved IP
        /// </summary>
        /// <param name="ip">string in format of ipv4 or ipv6</param>
        /// <returns>true if reserved, otherwise false</returns>
        /// <exception cref="ArgumentException">In case of wrong ip format</exception>
        public static bool IsReservedIpAddress (this string ip) {
            if (!string.IsNullOrEmpty (ip) && IPAddress.TryParse (ip, out var ipAddress)) {
                return ipAddress.IsReservedAddress ();
            }

            throw new ArgumentException ("wrong ip format", nameof (ip));
        }
    }
}