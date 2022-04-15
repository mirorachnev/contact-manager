import { Contact } from '../models/contact';

class ContactsService {

  APP_API_URL = "http://localhost:5209/api/contacts";

  constructor() {

  }

  getContacts = async (): Promise<Contact[]> => {
    const response = await fetch(this.APP_API_URL);

    const result = await response.json();

    return result;
  }

  getContact = async (id: string): Promise<Contact> => {
    const response = await fetch(`${this.APP_API_URL}/${id}`);

    const result = await response.json();

    return result;
  }

  deleteContact = async (id: string): Promise<void> => {
    await fetch(`${this.APP_API_URL}/${id}`, {
      method: "DELETE"
    })
  }

  createContact = async (contact: Contact): Promise<void> => {
    await fetch(this.APP_API_URL, {
      method: "POST",
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(contact)
    })
  }
}

export default new ContactsService();