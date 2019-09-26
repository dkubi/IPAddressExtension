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

using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAddressExtension.UnitTest
{
    [TestClass()]
    public class ReservedAddressTest
    {
        /// <summary>
        ///Die erste und letzte IPv4-Adresse eines Subnetzes sind reserviert
        /// x.x.x.0: Netz- oder Subnetz-Adresse
        ///</summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4NetOrSubNetTests()
        {
            var ip = IPAddress.Parse("195.1.1.0");
            Assert.IsTrue(ip.IsReservedAddress());
            ip = IPAddress.Parse("19.1.1.0");
            Assert.IsTrue(ip.IsReservedAddress());
        }

        /// <summary>
        ///Die erste und letzte IPv4-Adresse eines Subnetzes sind reserviert
        /// x.x.x.255: Broadcast-Adresse
        ///</summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4BroadcastTests()
        {
            var ip = IPAddress.Parse("195.1.1.255");
            Assert.IsTrue(ip.IsReservedAddress());
            ip = IPAddress.Parse("19.1.1.255");
            Assert.IsTrue(ip.IsReservedAddress());
        }

        /// <summary>
        /// Nicht routbare IPv4-Adressen
        /// 0.0.0.0/8 (0.0.0.0 bis 0.255.255.255): Standard- bzw.Default-Route im Subnetz(Current Network).
        /// 127.0.0.0/8 (127.0.0.0 bis 127.255.255.255): Reserviert f�r den Local Loop bzw.Loopback.
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4NotRoutableTests()
        {
            var ip = IPAddress.Parse("0.0.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Standard- bzw. Default-Route im Subnetz (Current Network)");
            ip = IPAddress.Parse("0.255.255.5");
            Assert.IsTrue(ip.IsReservedAddress(), "Standard- bzw. Default-Route im Subnetz (Current Network)");
            ip = IPAddress.Parse("0.255.255.255");
            Assert.IsTrue(ip.IsReservedAddress(), "Standard- bzw. Default-Route im Subnetz (Current Network)");
            ip = IPAddress.Parse("127.255.255.255");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r den Local Loop bzw. Loopback.");
            ip = IPAddress.Parse("127.0.0.2");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r den Local Loop bzw. Loopback.");
        }

        /// <summary>
        ///             Private IPv4-Adressen
        /// 10.0.0.0/8 (10.0.0.0 bis 10.255.255.255): Reserviert f�r die Nutzung in privaten Netzwerken.Nicht im Internet routbar.
        /// 172.16.0.0/12 (172.16.0.0 bis 172.31.255.255): Reserviert f�r die Nutzung in privaten Netzwerken.Nicht im Internet routbar.
        /// 192.168.0.0/16 (192.168.0.0 bis 192.168.255.255): Reserviert f�r die Nutzung in privaten Netzwerken.Nicht im Internet routbar.
        /// 169.254.0.0/16 (169.254.0.0 bis 169.254.255.255): Link-local Adresses f�r IPv4LL.
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4PrivateTests()
        {
            var ip = IPAddress.Parse("10.0.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("10.255.255.5");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("10.255.255.254");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("172.16.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("172.16.0.1");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("172.31.4.5");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("172.31.255.200");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("192.168.1.1");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("192.168.0.1");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("192.168.4.5");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("192.168.255.200");
            Assert.IsTrue(ip.IsReservedAddress(), "Reserviert f�r die Nutzung in privaten Netzwerken. Nicht im Internet routbar.");
            ip = IPAddress.Parse("169.254.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Link-local Adresses f�r IPv4LL.");
            ip = IPAddress.Parse("169.254.10.10");
            Assert.IsTrue(ip.IsReservedAddress(), "Link-local Adresses f�r IPv4LL.");
            ip = IPAddress.Parse("169.254.255.254");
            Assert.IsTrue(ip.IsReservedAddress(), "Link-local Adresses f�r IPv4LL.");
        }

        /// <summary>
        /// Class D (Multicast)
        /// 224.0.0.0 bis 239.255.255.255: Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4MulticastTests()
        {
            var ip = IPAddress.Parse("224.0.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.");
            ip = IPAddress.Parse("239.255.255.255");
            Assert.IsTrue(ip.IsReservedAddress(), "Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.");
            ip = IPAddress.Parse("239.255.255.254");
            Assert.IsTrue(ip.IsReservedAddress(), "Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.");
            ip = IPAddress.Parse("239.255.255.2");
            Assert.IsTrue(ip.IsReservedAddress(), "Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.");
            ip = IPAddress.Parse("224.1.1.1");
            Assert.IsTrue(ip.IsReservedAddress(), "Nicht im Internet, sondern nur lokal in den eigenen Netzen routbar.");
        }

        /// <summary>
        /// Class E (reservierte Adressen)
        /// 240.0.0.0 bis 255.255.255.255: Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv4ReservedAddressesTests()
        {
            var ip = IPAddress.Parse("240.0.0.0");
            Assert.IsTrue(ip.IsReservedAddress(), "Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.");
            ip = IPAddress.Parse("255.255.255.255");
            Assert.IsTrue(ip.IsReservedAddress(), "Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.");
            ip = IPAddress.Parse("240.255.255.254");
            Assert.IsTrue(ip.IsReservedAddress(), "Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.");
            ip = IPAddress.Parse("240.255.255.2");
            Assert.IsTrue(ip.IsReservedAddress(), "Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.");
            ip = IPAddress.Parse("255.1.1.1");
            Assert.IsTrue(ip.IsReservedAddress(), "Alte IPv4-Stacks, die nur mit Netzklassen arbeiten, kommen damit nicht klar.");
        }

        /// <summary>
        /// Adresse: ::1/128
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6LoopbackTests()
        {
            var ip = IPAddress.Parse("::1");
            Assert.IsTrue(ip.IsReservedAddress(), "Loopback");
        }

        /// <summary>
        /// Link Local Unicast
        /// Adressraum: fe80::/10 -- fe80:: - febf::
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6LinkLocalUnicastTests()
        {
            var ip = IPAddress.Parse("fe80::");
            Assert.IsTrue(ip.IsReservedAddress(), "Link Local Unicast");
            ip = IPAddress.Parse("febf::");
            Assert.IsTrue(ip.IsReservedAddress(), "Link Local Unicast");
        }

        /// <summary>
        /// Unique Local Unicast
        /// Adressraum: fe80::/10 -- fe80:: - febf::
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6UniqueLocalUnicastTests()
        {
            var ip = IPAddress.Parse("fc00::");
            Assert.IsTrue(ip.IsReservedAddress(), "Unique Local Unicast");
            ip = IPAddress.Parse("fdff::");
            Assert.IsTrue(ip.IsReservedAddress(), "Unique Local Unicast");
        }

        /// <summary>
        /// Multicast
        /// Adressraum: ff00::/8 -- ff00:: - ffff::
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6MulticastTests()
        {
            var ip = IPAddress.Parse("ff01::1");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff02::1");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff02::1");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff00::");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ffff::");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff01::2");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff02::2");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
            ip = IPAddress.Parse("ff05::2");
            Assert.IsTrue(ip.IsReservedAddress(), "Multicast");
        }

        /// <summary>
        /// Global Unicast
        ///     0:0:0:0:0:ffff::/96 -- IPv4 mapped (abgebildet)
        /// 2000::/3 -- von der IANA an die RIRs vergebene Netze
        /// 2002::/4 -- f�r den Tunnelmechanismus 6to4
        /// 2001:db8::/32 -- f�r Dokumentationszwecke
        /// </summary>
        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6GlobalUnicastTests()
        {
            //var ip = IPAddress.Parse("0:0:0:0:0:ffff::");
            //Assert.IsTrue(ip.IsReservedAddress(), " IPv4 mapped (abgebildet)");
            //var ip = IPAddress.Parse("2002::");
            //Assert.IsTrue(ip.IsReservedAddress(), "f�r den Tunnelmechanismus 6to4");
            var ip = IPAddress.Parse("2001:db8::");
            Assert.IsTrue(ip.IsReservedAddress(), "f�r Dokumentationszwecke");
        }

        [TestMethod()]
        [TestCategory("Gated")]
        public void Ipv6NotReserved()
        {
            var ip = IPAddress.Parse("2003:D4:BC0:0:0:0:0:0");
            Assert.IsFalse(ip.IsReservedAddress(), "Ip 2003:D4:BC0:0:0:0:0:0 should be valid");
        }
    }
}
