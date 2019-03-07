# OAuth and the Aras RESTful API

This project provides a sample .NET Core console application that demonstrates how to use the Aras Innovator OAuth server with the Aras RESTful API.

>Note: This sample application is not intended to be used as-is. Its purpose is to provide sample code that can be tested and adapted for an actual use case. Future releases of this project may include a sample with a client GUI. If you're looking for a client implemented with a specific framework or language, file an enhancement issue on this repo and we'll check it out.

## History

Release | Notes
--------|--------
[v1.0.0](https://github.com/ArasLabs/rest-auth-example/releases/tag/v1.0.0) | First release. Tested on Aras 11 SP15. 

#### Supported Aras Versions

Project | Aras
--------|------
[v1.0.0](https://github.com/ArasLabs/rest-auth-example/releases/tag/v1.0.0) | 11.0 SP14, 11.0 SP15

## Installation

#### Important!
**Always back up your code tree and database before applying an import package or code tree patch!**

### Pre-requisites

1. Aras Innovator installed
2. .NET Core installed

### Build the Console Application

1. Open `ConsoleApp\ODataExample.sln` in Visual Studio.
2. Select **Program.cs** from the Solution Explorer.
3. Update the constants with credentials for your Aras Innovator instance:

```
const string innovatorUrl = "http://localhost/InnovatorServer"; // base Innovator url
const string innovatorUsername = "admin"; // Innovator user name
const string innovatorPassword = "607920B64FE136F9AB2389E371852AF2"; // MD5 hash of Innovator user password
const string innovatorDatabase = "InnovatorSolutions"; // database name
const string oauthServerClientId = "IOMApp"; // must be registered in authorization server's oauth.config file
```

>Note: the oauthServerClientId must be registered in the authorization server's `OAuthServer\OAuth.config` file. The IOMApp is registered by default, so you don't need to change this value unless you want to add a custom application.

4. Save your changes to Program.cs.
5. From the **Build** menu, select **Build ODataExample**.

Once build has executed, you can find the resulting dll in `ConsoleApp\bin\Debug\netcoreapp2.1`.

## Usage

This sample application is not intended to be used as-is. Its purpose is to provide sample code that can be tested and adapted for an actual use case. However, here are some steps for testing the built dll and confirming the REST request succeeds.

1. Open a command prompt window.
2. Navigate to the location of the ODataExample.dll file (`ConsoleApp\bin\Debug\netcoreapp2.1`).
3. Run `dotnet ODataExample.dll`.

The result of the REST call will be printed to the command prompt window.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

For more information on contributing to this project, another Aras Labs project, or any Aras Community project, shoot us an email at araslabs@aras.com.

## Credits

Created by Aras Development.

### Other Contributors
* Eli Donahue, Aras Labs @EliJDonahue

## License

Aras Labs projects are published to Github under the MIT license. See the [LICENSE file](./LICENSE.md) for license rights and limitations.)