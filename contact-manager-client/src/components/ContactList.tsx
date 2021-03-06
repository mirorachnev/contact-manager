import React, { Component } from 'react';
import { Button, ButtonToolbar, Table } from 'react-bootstrap';
import { Contact } from '../models/contact';
import { AddContact } from './AddContact';
import { EditContact } from './EditContact';
import contactsService from '../services/contactsService';

export class ContactList extends Component {
  state: { contacts: Contact[], addModalShow: boolean, editModalShow: boolean, contact: Contact };

  constructor(props: {} | Readonly<{}>) {
    super(props);
    this.state = {
      contacts: [],
      addModalShow: false,
      editModalShow: false,
      contact: {
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        address: '',
        dateOfBirth: '',
        iban: '',
        id: ''
      }
    };
  }

  getContacts = async () => {
    
    const contacts = await contactsService.getContacts();
    this.setState({ contacts: contacts });
  }

  deleteContact = async (id: string) => {
    if (window.confirm('Are you sure?')) {
      await contactsService.deleteContact(id);

      this.getContacts();
    }    
  }

  componentDidMount() {
    this.getContacts();
  }

  render() {

    return (
      <>
        <Table className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Email</th>
              <th>Phone Number</th>
              <th>Date of Birth</th>
              <th>Address</th>
              <th>IBAN</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {this.state.contacts.map(contact =>
              <tr key={contact.id}>
                <td>{contact.firstName}</td>
                <td>{contact.lastName}</td>
                <td>{contact.email}</td>
                <td>{contact.phoneNumber}</td>
                <td>{new Date(contact.dateOfBirth).toLocaleDateString()}</td>
                <td>{contact.address}</td>
                <td>{contact.iban}</td>
                <td>
                  <ButtonToolbar>
                    <Button className="mr-2" variant="info"
                      onClick={() => this.setState({ editModalShow: true, contact: contact })}>
                      
                      Edit
                    </Button>                    

                    <Button className="mr-2" variant="danger"
                      onClick={() => this.deleteContact(contact.id)}>
                      Delete
                    </Button>

                    
                  </ButtonToolbar>

                </td>

              </tr>)}
          </tbody>

        </Table>

        <ButtonToolbar>
          <Button variant='primary'
            onClick={() => { this.setState({ addModalShow: true }); }}>
            Add Contact</Button>          
          
        </ButtonToolbar>

        <AddContact
          show={this.state.addModalShow}
          onHide={() => this.setState({ addModalShow: false })}
          reloadData={() => this.getContacts()}
        />

        <EditContact
          show={this.state.editModalShow}
          onHide={() => this.setState({ editModalShow: false })}
          reloadData={() => this.getContacts()}
          contact={this.state.contact}
        />
        
      </>
    )
  }
}