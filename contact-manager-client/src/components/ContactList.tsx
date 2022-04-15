import React, { Component } from 'react';
import { Button, ButtonToolbar, Table } from 'react-bootstrap';
import { Contact } from '../models/contact';
import { AddContact } from './AddContact';
import { EditContact } from './EditContact';
import contactsService from '../services/contactsService';

export class ContactList extends Component {
  state: { contacts: Contact[], addModalShow: boolean, editModalShow: boolean };

  constructor(props: {} | Readonly<{}>) {
    super(props);
    this.state = {
      contacts: [],
      addModalShow: false,
      editModalShow: false
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
            </tr>
          </thead>
          <tbody>
            {this.state.contacts.map(contact =>
              <tr key={contact.id}>
                <td>{contact.firstName}</td>
                <td>{contact.lastName}</td>
                <td>{contact.email}</td>
                <td>{contact.phoneNumber}</td>
                <td>{contact.dateOfBirth}</td>
                <td>{contact.address}</td>
                <td>{contact.iban}</td>
                <td>
                  <ButtonToolbar>
                    <Button className="mr-2" variant="info"
                      onClick={() => this.setState({ editModalShow: true})}>
                      
                      Edit
                    </Button>

                    <EditContact
                      show={this.state.editModalShow}
                      onHide={() => this.setState({ editModalShow: false })}
                      reloadData={() => this.getContacts()}
                      id={contact.id}
                    />

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

          <AddContact
            show={this.state.addModalShow}
            onHide={() => this.setState({ addModalShow: false })}
            reloadData={() => this.getContacts()}
            id={''}
          />
          
        </ButtonToolbar>
        
      </>
    )
  }
}