# Testing Evidence for Password Manager API

This document outlines the testing of the API endpoints for the Password Manager application using Insomnia.

---

## **1. POST /passwords**

### **Add new password entries:**

<img src="testingEvidence/1.1.png" alt="Alt Text" title="Optional Title" width="600">
<img src="testingEvidence/1.2.png" alt="Alt Text" title="Optional Title" width="600">
<img src="testingEvidence/1.3.png" alt="Alt Text" title="Optional Title" width="600">
<img src="testingEvidence/1.4.png" alt="Alt Text" title="Optional Title" width="600">



### **Add new password entry with invalid input:**

<img src="testingEvidence/1.5.png" alt="Alt Text" title="Optional Title" width="600">

### **Add new password entry with existing id:**

<img src="testingEvidence/1.6.png" alt="Alt Text" title="Optional Title" width="600">



---

## **2. GET /passwords/{username}**

### **Get a password entry by username:**

<img src="testingEvidence/2.1.png" alt="Alt Text" title="Optional Title" width="600">



---
## **3. GET /passwords/item/{id}**

### **Get a password entry by id:**

<img src="testingEvidence/3.1.png" alt="Alt Text" title="Optional Title" width="600">



---

## **4. PUT /passwords/item/{id}**

### **Update password by id:**

<img src="testingEvidence/4.1.png" alt="Alt Text" title="Optional Title" width="600">



---

## **5. DELETE /passwords/item/{id}**

### **Delete password by id:**


<img src="testingEvidence/5.1.png" alt="Alt Text" title="Optional Title" width="600">
<img src="testingEvidence/5.2.png" alt="Alt Text" title="Optional Title" width="600">


---

## **6. GET /passwords/item/{id}/decrypted**

### **Get decrypted password by id:**

<img src="testingEvidence/6.1.png" alt="Alt Text" title="Optional Title" width="600">
<img src="testingEvidence/6.2.png" alt="Alt Text" title="Optional Title" width="600">



---