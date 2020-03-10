#if UNITY_MACOSX && UNITY_DOTSPLAYER_IL2CPP_WAIT_FOR_MANAGED_DEBUGGER

#include <Unity/Runtime.h>

#include <CoreFoundation/CoreFoundation.h>

void DialogUpdateCallback()
{
// #if ENABLE_PLAYERCONNECTION
//     PlayerConnection::Get().Poll();
// #endif
}

DOTS_EXPORT(void)
ShowDebuggerAttachDialog(const char* message)
{
    CFStringRef msg = CFStringCreateWithCString(kCFAllocatorDefault, message, kCFStringEncodingUTF8);

    const void* keys[] = {kCFUserNotificationAlertHeaderKey, kCFUserNotificationAlertMessageKey, kCFUserNotificationDefaultButtonTitleKey};
    const void* values[] = {CFSTR("Debug"), msg, CFSTR("Ok")};
    CFDictionaryRef parameters = CFDictionaryCreate(0, keys, values, sizeof(keys) / sizeof(*keys),
        &kCFTypeDictionaryKeyCallBacks, &kCFTypeDictionaryValueCallBacks);

    CFUserNotificationRef userNotification;
    SInt32 err32;
    userNotification =  CFUserNotificationCreate(NULL, 0, kCFUserNotificationPlainAlertLevel, &err32, parameters);

    CFOptionFlags responseFlags = 0;
    while (CFUserNotificationReceiveResponse(userNotification, 1, &responseFlags))
    {
        DialogUpdateCallback();
    }
    CFRelease(userNotification);
    CFRelease(parameters);

    CFRelease(msg);
}

#endif
