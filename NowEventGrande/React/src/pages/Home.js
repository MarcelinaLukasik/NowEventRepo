import Banner from "../componentPages/Banner.js";
import { Benefit } from "../componentPages/Benefit.js";
import { Features } from "../componentPages/Features.js";
import { Contact } from "../componentPages/Contact.js";
import { Footer } from "../componentPages/Footer.js";
import Layout from "./Layout.js";
function Home() {
    return (
        <div>
            <Banner />
            <Benefit />
            <Features />
            <Contact />
            <Footer />
        </div>
    )
}

export default Home;