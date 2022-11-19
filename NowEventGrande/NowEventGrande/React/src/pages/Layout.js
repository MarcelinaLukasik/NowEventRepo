import { Outlet, Link } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useEffect, useState } from "react";
import facebookIcon from '../images/facebook.svg'
import instagramIcon from '../images/instagram.svg'
import twitterIcon from '../images/twitter.svg'
import '../styles/layout.css';
const Layout = () => {

  const [activeLink, setActiveLink] = useState('home');
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => scrollNavbar(), []);

  const scrollNavbar = () => {
    const onScroll = () => {
      if (window.scrollY > 55) {
        setScrolled(true);
      } else {
        setScrolled(false);
      }
    }

    window.addEventListener("scroll", onScroll);

    return () => window.removeEventListener("scroll", onScroll);
  }

  const onUpdateActiveLink = (link) => {
    setActiveLink(link);
    console.log(link);
  }

  return (
    <div>
      <Navbar expand="lg" className={scrolled ? "scrolled" : ""}>
        <Container>
          <Navbar.Brand as={Link} to="/"> nowEvent
            {/* <img src={""} alt="NowEvent" /> */}
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link as={Link} to="/" className={activeLink === 'home' ? 'active navbar-link' : 'navbar-link'} onClick={() => onUpdateActiveLink('home')}>Home</Nav.Link>
              <Nav.Link as={Link} to="/inspirations" >Get inspired</Nav.Link>
              <Nav.Link as={Link} to="/contact" className={activeLink === 'contact' ? 'active navbar-link' : 'navbar-link'} onClick={() => onUpdateActiveLink('contact')}>Contact</Nav.Link>
              <NavDropdown className="dropdown" title="Dropdown" id="basic-nav-dropdown">
                <NavDropdown.Item href="user">User</NavDropdown.Item>
                <NavDropdown.Item href="user2">User2</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item href="#action/3.4">
                  Separated link
                </NavDropdown.Item>
              </NavDropdown>
            </Nav>
            <span className="navbar-text">
              <div className="social-icon">
                <a href="#"><img src={facebookIcon} alt="facebook" /></a>
                <a href="#"><img src={instagramIcon} alt="instagram" /></a>
                <a href="#"><img src={twitterIcon} alt="twitter" /></a>
              </div>
              <button className="" onClick={() => console.log('sing in')}><span>Sign in</span></button>
            </span>
          </Navbar.Collapse>
        </Container>

      </Navbar>
      <div>
        <Outlet />
      </div>
    </div>
  )
};



export default Layout;